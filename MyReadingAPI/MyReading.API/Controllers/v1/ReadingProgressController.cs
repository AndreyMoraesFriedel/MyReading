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
                progressView.BookId,
                progressView.PagesRead,
                progressView.StartDate,
                progressView.EndDate
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

        //[Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ReadingProgressViewModel progressView)
        {
            var existingProgress = _readingProgressRepository.GetById(id);
            if (existingProgress == null)
            {
                return NotFound();
            }

            existingProgress.PagesRead = progressView.PagesRead;
            existingProgress.StartDate = progressView.StartDate;
            existingProgress.EndDate = progressView.EndDate;

            _readingProgressRepository.Update(existingProgress);
            var updatedProgressDTO = _mapper.Map<ReadingProgressDTO>(existingProgress);
            return Ok(updatedProgressDTO);
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
