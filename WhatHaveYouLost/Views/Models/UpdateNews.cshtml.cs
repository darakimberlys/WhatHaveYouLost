using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WhatYouHaveLost.Views.Models;

public class UpdateModel : PageModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string ImageLink { get; set; }
    public string AuthorLink { get; set; }
    
    public string Token { get; set; }
}