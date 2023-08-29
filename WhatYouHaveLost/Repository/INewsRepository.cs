using Microsoft.EntityFrameworkCore.ChangeTracking;
using WhatYouHaveLost.Repository.Data;

namespace WhatYouHaveLost.Repository;

public interface INewsRepository
{
    Task<News> GetCompleteNewsByIdAsync(string selectedNews);
    Task<List<News>> ReadAllNewsAsync();
    Task CreateNewsAsync(News news);
    void DeleteNews(News news);
}