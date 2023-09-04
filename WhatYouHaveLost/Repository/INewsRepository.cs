using Microsoft.EntityFrameworkCore.ChangeTracking;
using WhatYouHaveLost.Repository.Data;

namespace WhatYouHaveLost.Repository;

public interface INewsRepository
{
    Task<News> GetCompleteNewsByIdAsync(int selectedNews);
    List<News> ReadAllNews();
    Task CreateNewsAsync(News news);
    void DeleteNews(News news);
}