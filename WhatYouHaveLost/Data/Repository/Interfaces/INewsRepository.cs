using WhatYouHaveLost.Model.Data;

namespace WhatYouHaveLost.Data.Repository.Interfaces;

public interface INewsRepository
{
    Task<News> GetCompleteNewsByIdAsync(int selectedNews);
    List<News> ReadAllNews();
    void CreateNewsAsync(News news);
    void DeleteNews(int id);
    Task SaveChangesForNews();
}