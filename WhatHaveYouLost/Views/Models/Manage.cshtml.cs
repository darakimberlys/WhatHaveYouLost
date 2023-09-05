using Microsoft.AspNetCore.Mvc.RazorPages;
using WhatYouHaveLost.Model.Data;

namespace WhatYouHaveLost.Views.Models;

public class ManageModel : PageModel
{
    public List<News> NewsItems { get; set; }
    public string Token { get; set; }
}