using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WhatYouHaveLost.Repository;

namespace WhatYouHaveLost.Pages;

public class Details : PageModel
{
    private readonly ILogger<Details> _logger;
    private readonly INewsRepository _newsRepository;

    public Details(ILogger<Details> logger)
    {
        _logger = logger;
    }
    
    public IActionResult PalavraClicada(string palavra)
    {
        var result = _newsRepository.GetNewsContent(palavra);
        return RedirectToPage("detalhes", new {result});
    }
}