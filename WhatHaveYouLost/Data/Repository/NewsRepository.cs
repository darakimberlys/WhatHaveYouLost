using Microsoft.EntityFrameworkCore;
using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Model.Data;

namespace WhatYouHaveLost.Data.Repository;

public class NewsRepository : INewsRepository
{
    private readonly ApplicationDbContext _context;

    public NewsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<News> GetCompleteNewsByIdAsync(int selectedNews)
    {
        return await _context.News.FirstOrDefaultAsync(news => news.Id == selectedNews);
    }

    public List<News> ReadAllNews()
    {
        try
        {
           return _context.News.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task CreateNewsAsync(News news)
    {
        await _context.News.AddAsync(news);
    }

    public void DeleteNews(News news)
    {
        _context.News.Remove(news);
    }

    public void UpdateNews(News news)
    {
        _context.News.Update(news);
    }
}