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
        return await _context.News.FindAsync(selectedNews);
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

    public void CreateNewsAsync(News news)
    {
        _context.News.Add(news);
    }

    public async Task DeleteNews(int id)
    {
        var newsToDelete = await GetCompleteNewsByIdAsync(id);
        
        _context.News.Remove(newsToDelete);

        await SaveChangesForNews();
    }

    public async Task SaveChangesForNews()
    {
        await _context.SaveChangesAsync();
    }
}