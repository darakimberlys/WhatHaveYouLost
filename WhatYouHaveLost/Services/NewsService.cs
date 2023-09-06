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

    public async Task UpdateNewsAsync(UpsertModel model)
    {
        var news = new News();
        
        news = await _newsRepository.GetCompleteNewsByIdAsync(model.Id);
        
        if (news.Title != model.Title && model.Title is not null)
        {
            news.Title = model.Title;
            news.PublishDate = DateTime.Today;
        }
        
        if (news.Content != model.Content && model.Content is not null)
        {
            news.Content = model.Content;
            news.PublishDate = DateTime.Today;
        }
        
        if (news.Image != model.ImageLink && model.Content is not null)
        {
            news.Image = model.ImageLink;
            news.PublishDate = DateTime.Today;
        }
        
        if (news.Author != model.AuthorLink && model.Content is not null)
        {
            news.Author = model.AuthorLink;
            news.PublishDate = DateTime.Today;
        }
        
        await _newsRepository.SaveChangesForNews();
    }
}