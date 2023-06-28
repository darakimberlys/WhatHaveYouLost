using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WhatYouHaveLost.Repository;
using WhatYouHaveLost.Repository.Data;

namespace WhatYouHaveLost.Pages;

public class News : PageModel
{
    private readonly ILogger<News> _logger;
    private readonly INewsRepository _newsRepository;

    public News(ILogger<News> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Palavra(string palavra)
    {
        var result = new NewsData
        {
            Content = "teste content",
            Image = "chrome-extension://jgnejnfdbomaelibbccppknilnnhklnk/icons/CamFlip-Dark-64.png",
            Font = "globo",
            Title = "Não ha",
            NewsName = palavra
        };
        //var result = _newsRepository.GetNewsContent(palavra);
        return RedirectToPage("detalhes", new {result});
    }

    public void OnGet()
    {
        
    }
}