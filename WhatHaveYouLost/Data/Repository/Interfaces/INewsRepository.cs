using WhatYouHaveLost.Model.Data;

namespace WhatYouHaveLost.Data.Repository.Interfaces;

public interface INewsRepository
{
    Task<News> GetCompleteNewsByIdAsync(int selectedNews);
    List<News> ReadAllNews();
    Task CreateNewsAsync(News news);
    void DeleteNews(News news);
    void UpdateNews(News news);
}