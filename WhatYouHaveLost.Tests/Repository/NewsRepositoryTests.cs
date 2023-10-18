using AutoFixture;
using Moq;
using WhatYouHaveLost.Data.Repository;
using WhatYouHaveLost.Data.Repository.Interfaces;
using WhatYouHaveLost.Model.Data;

namespace WhatYouHaveLost.Tests.Repository;

public class NewsRepositoryTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<ApplicationDbContext> _applicationDbContext = new();
    
    private readonly INewsRepository _newsRepository;

    public NewsRepositoryTests()
    {
        _newsRepository = new NewsRepository(_applicationDbContext.Object);
    }

    [Fact]
    public async Task GetCompleteNewsByIdAsyncSuccess()
    {
        var news = _fixture.Create<News>();
        var id = news.Id;
        
        _applicationDbContext.Setup(x =>
                x.News.FindAsync(id))
            .ReturnsAsync(news);

        var result = await _newsRepository.GetCompleteNewsByIdAsync(id);
        
        Assert.Equal(news, result);
    }
}