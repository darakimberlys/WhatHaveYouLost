using WhatYouHaveLost.Repository.Data;

namespace WhatYouHaveLost.Repository;

public interface INewsRepository
{
    Task<NewsData> GetNewsContent(string selectedNews);
}