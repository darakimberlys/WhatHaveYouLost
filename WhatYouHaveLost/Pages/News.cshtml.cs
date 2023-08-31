using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhatYouHaveLost.Repository;

namespace WhatYouHaveLost.Pages;

public class NewsModel : PageModel
{
    private readonly INewsRepository _newsRepository;

    public List<Repository.Data.News> NewsList { get; set; }

    public NewsModel(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    public IActionResult Id(string id)
    {
        return RedirectToPage("detalhes", new { id });
    }

    public async Task OnGet()
    {
        NewsList = await _newsRepository.ReadAllNewsAsync();
    }
}