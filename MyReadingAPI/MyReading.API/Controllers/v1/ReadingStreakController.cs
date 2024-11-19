﻿using Microsoft.AspNetCore.Authorization;
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
    [Route("api/v{version:apiVersion}/reading-streak")]
    [ApiVersion("1.0")]
    public class ReadingStreakController : ControllerBase
    {
        private readonly IReadingStreakRepository _readingStreakRepository;
        private readonly IMapper _mapper;

        public ReadingStreakController(IReadingStreakRepository readingStreakRepository, IMapper mapper)
        {
            _readingStreakRepository = readingStreakRepository ?? throw new ArgumentNullException(nameof(readingStreakRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        //[Authorize]
        [HttpPost]
        public IActionResult Add([FromBody] ReadingStreakViewModel streakView)
        {
            var streak = new ReadingStreak(
                streakView.Id,
                streakView.UserId,
                streakView.StartDate,
                streakView.EndDate,
                streakView.LengthInDays
            );
            _readingStreakRepository.Add(streak);
            return Ok();
        }

        //[Authorize]
        [HttpGet]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            var streaks = _readingStreakRepository.Get(pageNumber, pageQuantity);
            var streakDTOs = _mapper.Map<IEnumerable<ReadingStreakDTO>>(streaks);
            return Ok(streakDTOs);
        }

        //[Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var streak = _readingStreakRepository.GetById(id);
            if (streak == null)
            {
                return NotFound();
            }

            var streakDTO = _mapper.Map<ReadingStreakDTO>(streak);
            return Ok(streakDTO);
        }

        //[Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ReadingStreakViewModel streakView)
        {
            var existingStreak = _readingStreakRepository.GetById(id);
            if (existingStreak == null)
            {
                return NotFound();
            }

            existingStreak.StartDate = streakView.StartDate;
            existingStreak.EndDate = streakView.EndDate;
            existingStreak.LengthInDays = streakView.LengthInDays;

            _readingStreakRepository.Update(existingStreak);
            var updatedStreakDTO = _mapper.Map<ReadingStreakDTO>(existingStreak);
            return Ok(updatedStreakDTO);
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var streak = _readingStreakRepository.GetById(id);
            if (streak == null)
            {
                return NotFound();
            }

            _readingStreakRepository.Delete(id);
            return Ok();
        }
    }
}
