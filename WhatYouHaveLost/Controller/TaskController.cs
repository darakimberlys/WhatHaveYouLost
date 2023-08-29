using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhatYouHaveLost.Repository;
using WhatYouHaveLost.Repository.Data;

namespace WhatYouHaveLost.Controller;

    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly INewsRepository _newsRepository;

        public TaskController(ApplicationDbContext context, INewsRepository newsRepository)
        {
            _context = context;
            _newsRepository = newsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<News>>> GetAllNewsAsync()
        {
            return await _newsRepository.ReadAllNewsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetNewsContentAsync(string id)
        {
            return await _newsRepository.GetCompleteNewsByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Task>> CreateNewsAsync(News news)
        {
            _context.NewsData.Add(news);
            await _context.SaveChangesAsync();

            return null;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTask(int id, Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }

