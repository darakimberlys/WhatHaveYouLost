using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhatYouHaveLost.Repository;
using WhatYouHaveLost.Repository.Data;

namespace WhatYouHaveLost.Pages;

public class Details : PageModel
{
    private readonly INewsRepository _newsRepository;

    public NewsData News { get; set; }

    public Details(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    public IActionResult OnGet(string id)
    {
        News = GetNewsById(id);

        return Page();
    }

    private NewsData GetNewsById(string id)
    {
        return _newsRepository.GetNewsContent(id);
    }
}