using Microsoft.AspNetCore.Mvc;
using WhatYouHaveLost.Repository;
using WhatYouHaveLost.Repository.Interfaces;
using WhatYouHaveLost.Views.Home;
using WhatYouHaveLost.Views.Models;

namespace WhatYouHaveLost.Controllers;

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INewsRepository _newsRepository;

        public HomeController(ApplicationDbContext context, INewsRepository newsRepository)
        {
            _context = context;
            _newsRepository = newsRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Login()
        {
            return View();
        }
        
        /// <summary>
        /// Visualizar todas as noticias no banco de dados
        /// </summary>
        /// <returns></returns>
        public IActionResult News()
        {
            var newsmodel = new NewsModel(_newsRepository);
            newsmodel.NewsList = _newsRepository.ReadAllNews();

            return View(newsmodel);
        }

        [Route("Home/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var detailsModel = new DetailsModel();
            detailsModel.News = await _newsRepository.GetCompleteNewsByIdAsync(id);

            return View(detailsModel);
        }
        
        // [HttpGet("{id}")]
        // public async Task<ActionResult<News>> GetNewsContentAsync(string id)
        // {
        //     return await _newsRepository.GetCompleteNewsByIdAsync(id);
        // }
        //
        // [HttpPost]
        // public async Task<ActionResult<Task>> CreateNewsAsync(News news)
        // {
        //     _context.News.Add(news);
        //     await _context.SaveChangesAsync();
        //
        //     return null;
        // }
        //
        // [HttpPatch("{id}")]
        // public async Task<IActionResult> UpdateTask(int id, Task task)
        // {
        //     if (id != task.Id)
        //     {
        //         return BadRequest();
        //     }
        //
        //     _context.Entry(task).State = EntityState.Modified;
        //
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!TaskExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }
        //
        //     return NoContent();
        // }
        //
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteTask(int id)
        // {
        //     var task = await _context.Tasks.FindAsync(id);
        //
        //     if (task == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.Tasks.Remove(task);
        //     await _context.SaveChangesAsync();
        //
        //     return NoContent();
        // }
        //
        // private bool TaskExists(int id)
        // {
        //     return _context.Tasks.Any(e => e.Id == id);
        // }
    }

