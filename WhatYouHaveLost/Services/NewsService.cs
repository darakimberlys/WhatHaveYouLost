using WhatYouHaveLost.Repository;
using WhatYouHaveLost.Repository.Data;
using WhatYouHaveLost.Services.Interface;

namespace WhatYouHaveLost.Services;

public class NewsService : INewsService
{
    private readonly INewsRepository _newsRepository;

    public NewsService(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    public void AddNews(News news)
    {
        news.PublishDate = DateTime.Now.Date;

        _newsRepository.CreateNewsAsync(news);
    }

    public async Task DeleteNews(int id)
    {
        var newsObject = await _newsRepository.GetCompleteNewsByIdAsync(id);
        _newsRepository.DeleteNews(newsObject);
    }
}