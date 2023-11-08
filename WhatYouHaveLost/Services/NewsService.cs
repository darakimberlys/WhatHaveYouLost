using Serilog.Context;
using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Model.Data;
using WhatYouHaveLost.Services.Interfaces;
using WhatYouHaveLost.Views.Models;

namespace WhatYouHaveLost.Services;

public class NewsService : INewsService
{
    private readonly INewsRepository _newsRepository;
    private readonly ILogger<NewsService> _logger;


    public NewsService(INewsRepository newsRepository, ILogger<NewsService> logger)
    {
        _newsRepository = newsRepository;
        _logger = logger;
    }

    public async Task<bool> CreateNewsAsync(CreateNewsModel model)
    {
        try
        {
            if (model.Content == null)
            {
                _logger.LogError("Invalid form");
                
                throw new ArgumentNullException(nameof(model));
            }

            _newsRepository.CreateNewsAsync(new News
            {
                Title = model.Title,
                Content = model.Content,
                Image = model.ImageLink,
                Author = model.AuthorLink,
                PublishDate = DateTime.Today
            });

            var resultForTest = await _newsRepository.SaveChangesForNews();
            return resultForTest != 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> UpdateNewsAsync(UpsertModel model)
    {
        try
        {
            if (model?.Content == null)
            {
                _logger.LogError("Invalid form");

                return false;
            }

            var news = new News();

            news = await _newsRepository.GetCompleteNewsByIdAsync(model.Id);

            if (news.Title != model?.Title && model?.Title is not null)
            {
                news.Title = model.Title;
                news.PublishDate = DateTime.Today;
            }

            if (news.Content != model?.Content && model.Content is not null)
            {
                news.Content = model.Content;
                news.PublishDate = DateTime.Today;
            }

            if (news.Image != model?.ImageLink && model?.ImageLink is not null)
            {
                news.Image = model.ImageLink;
                news.PublishDate = DateTime.Today;
            }

            if (news.Author != model?.AuthorLink && model?.AuthorLink is not null)
            {
                news.Author = model.AuthorLink;
                news.PublishDate = DateTime.Today;
            }

            var resultForTest = await _newsRepository.SaveChangesForNews();
            return resultForTest != 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            Console.WriteLine(e);
            throw;
        }
    }
}