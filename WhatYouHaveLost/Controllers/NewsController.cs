using Microsoft.AspNetCore.Mvc;
using WhatYouHaveLost.Repository;

namespace WhatYouHaveLost.Controllers;

[ApiController]
[Route("/GoodNews")]
public class NewsController
{
    private readonly INewsRepository _newsRepository;
    
    public NewsController(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetGoodNews(string news)
    {

        var selected = await _newsRepository.GetNewsContent(news);

        return OkResult(selected);
    }
}