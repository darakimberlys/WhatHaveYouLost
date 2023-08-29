using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WhatYouHaveLost.Repository;
using WhatYouHaveLost.Repository.Data;
using WhatYouHaveLost.Services.Interface;

namespace WhatYouHaveLost.Pages;

public class NewsManageModel : PageModel
{
    private readonly INewsRepository _newsRepository;
    private readonly INewsService _newsService;
    public List<News> NewsList { get; set; }

    public NewsManageModel(INewsRepository newsRepository, INewsService newsService)
    {
        _newsRepository = newsRepository;
        _newsService = newsService;
    }
    
    public async Task<IActionResult> OnGet(string? id)
    {
        NewsList = await GetNewsList();

        return Page();
    }

    private async Task<List<News>> GetNewsList()
    {
        NewsList = await _newsRepository.ReadAllNewsAsync();
        
        return NewsList;
    }
    
    private async Task<RedirectToPageResult> DeleteNewsAsync(string id)
    {
        await _newsService.DeleteNews(id);
        
        return RedirectToPage("managenews");
    }
    
    private IActionResult AddNews()
    {
        return RedirectToPage("addNews");
    }
    
    private IActionResult EditNewsById(string id)
    {
       return RedirectToPage("editNews", id); //TODO: Adicionar essa pagina com formulario
    }
}