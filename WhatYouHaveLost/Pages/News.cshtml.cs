using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhatYouHaveLost.Repository;
using WhatYouHaveLost.Repository.Data;

namespace WhatYouHaveLost.Pages;

public class News : PageModel
{
    private readonly INewsRepository _newsRepository;
   
    public List<NewsData> NewsList { get; set; } 

    public News(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }
    
    public IActionResult Id(string id)
    {
        return RedirectToPage("detalhes", new { id });
    }

    public void OnGet()
    {
        NewsList = _newsRepository.GetAllNews();
    }
}