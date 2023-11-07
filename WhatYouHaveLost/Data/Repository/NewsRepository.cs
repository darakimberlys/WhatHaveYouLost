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

    public void DeleteNews(int id)
    {
        var newsToDelete = _context.News.Find(id);
        if (newsToDelete != null)
        {
            _context.News.Remove(newsToDelete);

            _context.SaveChanges();
        }
    }

    public async Task<int> SaveChangesForNews()
    {
        return await _context.SaveChangesAsync();
    }
}