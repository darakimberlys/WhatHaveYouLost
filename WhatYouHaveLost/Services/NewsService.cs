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

    public async Task CreateNews(CreateNewsModel model)
    {
        try
        {
            _newsRepository.CreateNewsAsync(new News
            {
                Title = model.Title,
                Content = model.Content,
                Image = model.ImageLink,
                Author = model.AuthorLink,
                PublishDate = DateTime.Today
            });

            await _newsRepository.SaveChangesForNews();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task UpdateNews(UpsertModel model)
    {
        var news = new News();
        
        news = await _newsRepository.GetCompleteNewsByIdAsync(model.Id);
        
        if (news.Title != model.Title)
        {
            news.Title = model.Title;
            news.PublishDate = DateTime.Today;
        }
        
        if (news.Content != model.Content)
        {
            news.Content = model.Content;
            news.PublishDate = DateTime.Today;
        }
        
        if (news.Image != model.ImageLink)
        {
            news.Image = model.ImageLink;
            news.PublishDate = DateTime.Today;
        }
        
        if (news.Author != model.AuthorLink)
        {
            news.Author = model.AuthorLink;
            news.PublishDate = DateTime.Today;
        }
        
        await _newsRepository.SaveChangesForNews();
    }
}