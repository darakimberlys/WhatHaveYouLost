using WhatYouHaveLost.Repository.Data;

namespace WhatYouHaveLost.Services.Interface;

public interface INewsService
{
    void AddNews(News news);
    Task DeleteNews(string id);
}