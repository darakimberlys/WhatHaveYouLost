using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WhatYouHaveLost.Repository;
using WhatYouHaveLost.Repository.Data;

namespace WhatYouHaveLost.Pages;

public class Details : PageModel
{
    private readonly ILogger<Details> _logger;
    private readonly INewsRepository _newsRepository;

    public Details(ILogger<Details> logger)
    {
        _logger = logger;
    }

    public void OnGet(string palavraChave)
    {
        PalavraClicada(palavraChave);
    }
    
    private ActionResult<NewsData> PalavraClicada(string palavra)
    {
        //var result = _newsRepository.GetNewsContent(palavra);
        //return RedirectToPage("detalhes", new {result});
        var result = new NewsData
        {
            Content = "teste content",
            Image = "chrome-extension://jgnejnfdbomaelibbccppknilnnhklnk/icons/CamFlip-Dark-64.png",
            Font = "globo",
            Title = "Não ha",
            NewsName = palavra
        };
        return result;
    }
}