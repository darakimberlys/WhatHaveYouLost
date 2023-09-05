using WhatYouHaveLost.Model.Data;
using WhatYouHaveLost.Views.Models;

namespace WhatYouHaveLost.Services.Interfaces;

public interface INewsService
{
    void CreateNews(News news);
    Task DeleteNews(int id);
    Task UpdateNews(UpdateModel model);
}