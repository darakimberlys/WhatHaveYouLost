namespace WhatYouHaveLost.Repository;

public interface INewsRepository
{
    Task<IEnumerable<NewsData>> GetNewsContent(string selectedNews);
}