using Microsoft.AspNetCore.Mvc;
using MyReading.API.Infrastructure.Repository;
using MyReading.API.Domain.Model;
using MyReading.API.Application.ViewModel;
using AutoMapper;
using MyReading.API.Domain.DTOs;
using Asp.Versioning;


namespace MyReading.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/user")]
    [ApiVersion("1.0")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        //[Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] UserViewModel userView)
        {
            var filePath = Path.Combine("Storage", userView.Photo.FileName);

            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            userView.Photo.CopyTo(fileStream);

            // Gera o hash da senha antes de salvar
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userView.Password);

            var user = new User(userView.Id, userView.Name, userView.Email, hashedPassword, filePath);
            _userRepository.Add(user);
            return Ok();
        }

        //[Authorize]
        [HttpGet("{id}/download")]
        public IActionResult DownloadPhoto(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            var dataBytes = System.IO.File.ReadAllBytes(user.Photo);
            return File(dataBytes, "image/png");
        }

        //[Authorize]
        [HttpGet]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            var users = _userRepository.Get(pageNumber, pageQuantity);
            var usersDTO = _mapper.Map<IEnumerable<UserDTO>>(users);
            return Ok(usersDTO);
        }

        //[Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            var user = _userRepository.Authenticate(loginDto.Email, loginDto.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Email ou senha inválidos" });
            }

            return Ok(new
            {
                id = user.Id,
                name = user.Name,
                photo = user.Photo
            });
        }


        //[Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] UserViewModel userView)
        {
            var existingUser = _userRepository.GetById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Name = userView.Name;
            existingUser.Email = userView.Email;
            existingUser.Password = userView.Password;

            if (userView.Photo != null)
            {
                var filePath = Path.Combine("Storage", userView.Photo.FileName);

                // Delete old file if exists
                if (System.IO.File.Exists(existingUser.Photo))
                {
                    System.IO.File.Delete(existingUser.Photo);
                }

                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                userView.Photo.CopyTo(fileStream);
                existingUser.Photo = filePath;
            }

            _userRepository.Update(existingUser);
            var updatedUserDTO = _mapper.Map<UserDTO>(existingUser);
            return Ok(updatedUserDTO);
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.Delete(id);
            return Ok();
        }
    }
}
