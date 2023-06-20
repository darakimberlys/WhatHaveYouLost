using WhatYouHaveLost.Repository.Data;

namespace WhatYouHaveLost.Repository;

public interface INewsRepository
{
    NewsData GetNewsContent(string selectedNews);
}