using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyReading.API.Infrastructure.Repository;
using MyReading.API.Model;
using MyReading.API.ViewModel;

namespace MyReading.API.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] UserViewModel userView)
        {
            var filePath = Path.Combine("Storage", userView.Photo.FileName);

            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            userView.Photo.CopyTo(fileStream);

            var User = new User(userView.Id, userView.Name, userView.Email, userView.Password,filePath);
            _userRepository.Add(User);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/download")]
        public IActionResult DownloadPhoto(int id)
        {
            var user = _userRepository.GetById(id);
            var dataBytes = System.IO.File.ReadAllBytes(user.Photo);

            return File(dataBytes, "image/png");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            var users = _userRepository.Get(pageNumber, pageQuantity);
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [Authorize]
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
            return Ok(existingUser);
        }

        [Authorize]
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
