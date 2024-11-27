using Microsoft.AspNetCore.Authorization;
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
    [Route("api/v{version:apiVersion}/reading-progress")]
    [ApiVersion("1.0")]
    public class ReadingProgressController : ControllerBase
    {
        private readonly IReadingProgressRepository _readingProgressRepository;
        private readonly IMapper _mapper;

        public ReadingProgressController(IReadingProgressRepository readingProgressRepository, IMapper mapper)
        {
            _readingProgressRepository = readingProgressRepository ?? throw new ArgumentNullException(nameof(readingProgressRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        //[Authorize]
        [HttpPost]
        public IActionResult Add([FromBody] ReadingProgressViewModel progressView)
        {
            var progress = new ReadingProgress(
                progressView.Id,
                progressView.UserId,
                progressView.BookId
            );
            _readingProgressRepository.Add(progress);
            return Ok();
        }

        //[Authorize]
        [HttpGet]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            var progresses = _readingProgressRepository.Get(pageNumber, pageQuantity);
            var progressDTOs = _mapper.Map<IEnumerable<ReadingProgressDTO>>(progresses);
            return Ok(progressDTOs);
        }

        //[Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var progress = _readingProgressRepository.GetById(id);
            if (progress == null)
            {
                return NotFound();
            }

            var progressDTO = _mapper.Map<ReadingProgressDTO>(progress);
            return Ok(progressDTO);
        }

        [HttpGet("user/{userId}/book/{bookId}/total-reading-time")]
        public IActionResult GetTotalReadingTimeByUserAndBook(int userId, int bookId)
        {
            try
            {
                var totalReadingTime = _readingProgressRepository.GetTotalReadingTimeByUserAndBook(userId, bookId);
                return Ok(new { UserId = userId, BookId = bookId, TotalReadingTime = totalReadingTime });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        //[Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ReadingProgressViewModel progressView)
        {
            var existingProgress = _readingProgressRepository.GetById(id);
            if (existingProgress == null)
            {
                return NotFound();
            }

            existingProgress.TotalReadingTime = progressView.TotalReadingTime;

            _readingProgressRepository.Update(existingProgress);
            var updatedProgressDTO = _mapper.Map<ReadingProgressDTO>(existingProgress);
            return Ok(updatedProgressDTO);
        }

        [HttpPut("{userId}/book/{bookId}/increment-time")]
        public IActionResult IncrementReadingTime(int userId, int bookId, [FromQuery] string timeToAddInSeconds)
        {
            try
            {
                var timeToAdd = TimeSpan.Parse(timeToAddInSeconds);
                _readingProgressRepository.IncrementReadingTime(userId, bookId, timeToAdd);
                return Ok(new { message = "Tempo de leitura incrementado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        //[Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var progress = _readingProgressRepository.GetById(id);
            if (progress == null)
            {
                return NotFound();
            }

            _readingProgressRepository.Delete(id);
            return Ok();
        }
    }
}
