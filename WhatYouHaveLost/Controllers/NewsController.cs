using System.Text.Json;
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

    /// <summary>
    /// [HttpGet]
    /// </summary>
    /// <param name="news"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetGoodNews(string news)
    {
        try
        {
            var selected = _newsRepository.GetNewsContent(news);

            return new AcceptedResult("", $"{JsonSerializer.Serialize(selected)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}