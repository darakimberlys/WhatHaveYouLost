using WhatYouHaveLost.Repository.Data;

namespace WhatYouHaveLost.Repository.Interfaces;

public interface INewsRepository
{
    Task<News> GetCompleteNewsByIdAsync(int selectedNews);
    List<News> ReadAllNews();
    Task CreateNewsAsync(News news);
    void DeleteNews(News news);
}