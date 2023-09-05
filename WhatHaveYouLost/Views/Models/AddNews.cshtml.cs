using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhatYouHaveLost.Model.Data;
using WhatYouHaveLost.Services.Interfaces;

namespace WhatYouHaveLost.Views.Models;

public class AddNewsModel : PageModel
{
    private readonly INewsService _newsService;

    public AddNewsModel(INewsService newsService)
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