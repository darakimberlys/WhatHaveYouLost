using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Model.Data;
using WhatYouHaveLost.Services.Interfaces;
using WhatYouHaveLost.Views.Models;

namespace WhatYouHaveLost.Services;

public class NewsService : INewsService
{
    private readonly INewsRepository _newsRepository;

    public NewsService(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    public void CreateNews(News news)
    {
        news.PublishDate = DateTime.Now.Date;

        _newsRepository.CreateNewsAsync(news);
    }

    public async Task UpdateNews(UpdateModel model)
    {
        var news = new News();
        
        news = await _newsRepository.GetCompleteNewsByIdAsync(model.Id);
        
        if (!string.IsNullOrWhiteSpace(model.Title))
        {
            news.Title = model.Title;
            news.PublishDate = DateTime.Today;
        }
        
        if (!string.IsNullOrWhiteSpace(model.Content))
        {
            news.Content = model.Content;
            news.PublishDate = DateTime.Today;
        }
        
        if (!string.IsNullOrWhiteSpace(model.ImageLink))
        {
            news.Image = model.ImageLink;
            news.PublishDate = DateTime.Today;
        }
        
        if (!string.IsNullOrWhiteSpace(model.AuthorLink))
        {
            news.Author = model.AuthorLink;
            news.PublishDate = DateTime.Today;
        }

        if (news.PublishDate == DateTime.Today)
        {
            _newsRepository.UpdateNews(news);
        }
    }
    
    public async Task DeleteNews(int id)
    {
        var newsObject = await _newsRepository.GetCompleteNewsByIdAsync(id);
        _newsRepository.DeleteNews(newsObject);
    }
}