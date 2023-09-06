using WhatYouHaveLost.Model.Data;
using WhatYouHaveLost.Views.Models;

namespace WhatYouHaveLost.Services.Interfaces;

public interface INewsService
{
    Task CreateNews(CreateNewsModel model);
    Task UpdateNewsAsync(UpsertModel model);
}