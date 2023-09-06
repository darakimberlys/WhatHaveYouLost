using Microsoft.AspNetCore.Mvc.RazorPages;
using WhatYouHaveLost.Model.Data;

namespace WhatYouHaveLost.Views.Models;

public class NewsModel : PageModel
{
    public List<News> NewsList { get; set; }
}