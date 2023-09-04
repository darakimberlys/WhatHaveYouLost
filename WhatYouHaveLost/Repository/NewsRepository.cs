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

    public async Task<News> GetCompleteNewsByIdAsync(int selectedNews)
    {
        return await _context.News.FirstOrDefaultAsync(news => news.Id == selectedNews);
    }

    public List<News> ReadAllNews()
    {
        try
        {
            var teste =  _context.News.ToList();
            return teste;
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

    public void UpdateNews(News news, string id)
    {
        _context.News.Update(news);
    }
}