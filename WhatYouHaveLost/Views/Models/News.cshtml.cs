using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Model.Data;

namespace WhatYouHaveLost.Views.Models;

public class NewsModel : PageModel
{
    private readonly INewsRepository _newsRepository;
    
    public NewsModel(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }
    
    public List<News> NewsList { get; set; }
    
    public IActionResult OnPost(int id)
    {
        return RedirectToPage("/detalhes", new { id });
    }
    
    public void OnGet()
    {
        NewsList = _newsRepository.ReadAllNews();
    }
}