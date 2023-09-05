using WhatYouHaveLost.Model.Data;

namespace WhatYouHaveLost.Services.Interfaces;

public interface INewsService
{
    void AddNews(News news);
    Task DeleteNews(int id);
}