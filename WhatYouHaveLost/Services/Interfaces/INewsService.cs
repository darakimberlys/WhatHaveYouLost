using WhatYouHaveLost.Model.Data;
using WhatYouHaveLost.Views.Models;

namespace WhatYouHaveLost.Services.Interfaces;

public interface INewsService
{
    Task<bool> CreateNewsAsync(CreateNewsModel model);
    Task<bool>  UpdateNewsAsync(UpsertModel model);
}