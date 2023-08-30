using Microsoft.EntityFrameworkCore;
using WhatYouHaveLost.Repository.Data;

namespace WhatYouHaveLost.Repository;

public class NewsRepository : INewsRepository
{
    private readonly ApplicationDbContext _context;

    public NewsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<News> GetCompleteNewsByIdAsync(string selectedNews)
    {
        return await _context.NewsData.FirstOrDefaultAsync(news => news.Id == selectedNews);
    }

    public async Task<List<News>> ReadAllNewsAsync()
    {
        return await _context.NewsData.ToListAsync();
    }

    public async Task CreateNewsAsync(News news)
    {
        await _context.NewsData.AddAsync(news);
    }

    public void DeleteNews(News news)
    {
        _context.NewsData.Remove(news);
    }

    public void UpdateNews(News news, string id)
    {
        _context.NewsData.Update(news);
    }
}