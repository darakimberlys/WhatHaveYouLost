using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WhatYouHaveLost.Repository;

namespace WhatYouHaveLost.Pages;

public class News : PageModel
{
    private readonly ILogger<News> _logger;
    private readonly INewsRepository _newsRepository;

    public News(ILogger<News> logger)
    {
        _logger = logger;
    }
    
    public IActionResult PalavraClicada(string palavra)
    {
        var result = _newsRepository.GetNewsContent(palavra);
        return RedirectToPage("detalhes", new {result});
    }
    
    public void OnGet()
    {

    }
}