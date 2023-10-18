using AutoFixture;
using Moq;
using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Model.Data;
using WhatYouHaveLost.Services;
using WhatYouHaveLost.Services.Interfaces;
using WhatYouHaveLost.Views.Models;

namespace WhatYouHaveLost.Tests;

public class NewsServiceTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<INewsRepository> _newsRepository = new();
    private readonly INewsService _newsService;

    public NewsServiceTests()
    {
        _newsService = new NewsService(_newsRepository.Object);
    }

    [Theory]
    [InlineData("Success")]
    [InlineData("Exception")]
    [InlineData("NullException")]
    public async Task VerifyCreateNews(string expectedResult)
    {
        var model = _fixture.Create<CreateNewsModel>();
        
        _newsRepository.Setup(x =>
            x.CreateNewsAsync(It.IsAny<News>()));
        
        _newsRepository.Setup(x =>
            x.SaveChangesForNews());

        if (expectedResult != "Success")
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() 
                => _newsService.CreateNewsAsync(new CreateNewsModel()));

            Assert.Equal("model", exception.ParamName);
        }
        else
        {
            var result = await _newsService.CreateNewsAsync(model);

            Assert.True(result);
        }
    }
    
    [Theory]
    [InlineData("Success")]
    [InlineData("Exception")]
    [InlineData("NullException")]
    public async Task VerifyUpdateNews(string expectedResult)
    {
        var model = _fixture.Create<UpsertModel>();
        var news = _fixture.Create<News>();

        _newsRepository.Setup(x =>
            x.GetCompleteNewsByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(news);
        
        _newsRepository.Setup(x =>
            x.SaveChangesForNews());

        if (expectedResult != "Success")
        {
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() 
                => _newsService.UpdateNewsAsync(new UpsertModel()));

            Assert.Equal("model", exception.ParamName);
        }
        else
        {
            var result = await _newsService.UpdateNewsAsync(model);

            Assert.True(result);
        }
    }
}