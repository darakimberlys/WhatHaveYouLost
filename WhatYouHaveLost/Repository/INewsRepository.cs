using WhatYouHaveLost.Repository.Data;

namespace WhatYouHaveLost.Repository;

public interface INewsRepository
{
    Task<IEnumerable<NewsData>> GetNewsContent(string selectedNews);
}