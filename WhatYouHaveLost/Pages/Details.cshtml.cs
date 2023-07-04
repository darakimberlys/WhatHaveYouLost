using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WhatYouHaveLost.Repository;
using WhatYouHaveLost.Repository.Data;

namespace WhatYouHaveLost.Pages
{
    public class Details : PageModel
    {
        private readonly ILogger<Details> _logger;
        private readonly INewsRepository _newsRepository;

        public NewsData News { get; set; }

        public Details(ILogger<Details> logger, INewsRepository newsRepository)
        {
            _logger = logger;
            _newsRepository = newsRepository;
        }

        public IActionResult OnGet(string id)
        {
            News = Palavra(id);

            return Page();
        }

        private NewsData Palavra(string palavra)
        {
            return _newsRepository.GetNewsContent(palavra);
        }
    }
}