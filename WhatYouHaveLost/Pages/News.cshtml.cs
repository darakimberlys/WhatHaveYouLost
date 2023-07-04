using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhatYouHaveLost.Repository;

namespace WhatYouHaveLost.Pages;

public class News : PageModel
{
    private readonly ILogger<News> _logger;
    
    public News(ILogger<News> logger, INewsRepository newsRepository)
    {
        _logger = logger;
    }
    
    public IActionResult Palavra(string palavra)
    {
        return RedirectToPage("detalhes", new { palavra });
    }

    public void OnGet()
    { }
}