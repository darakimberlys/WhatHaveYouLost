using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WhatYouHaveLost.Pages;

public class GoodNewsSelection : PageModel
{
    private readonly ILogger<GoodNewsSelection> _logger;

    public GoodNewsSelection(ILogger<GoodNewsSelection> logger)
    {
        _logger = logger;
    }

    public List<string> Palavras { get; set; } = new()
    {
        "Palavra1",
        "Palavra2",
        "Palavra3"
    };

    public string PalavraSelecionada { get; set; }

    public void PalavraClicada(string palavra)
    {
        PalavraSelecionada = palavra;
    }
    public void OnGet()
    {
        
    }
}