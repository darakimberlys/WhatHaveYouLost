using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhatYouHaveLost.Repository.Data;
using WhatYouHaveLost.Services.Interface;

namespace WhatYouHaveLost.Pages
{
    public class EditNewsModel : PageModel
    {
        private readonly INewsService _newsService;

        public EditNewsModel(INewsService newsService)
        {
            _newsService = newsService;
        }

        [BindProperty]
        public News News { get; set; }

        public void OnGet()
        { }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            _newsService.AddNews(News);

            return RedirectToPage("news");
        }
    }
}