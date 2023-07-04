using System;

namespace WhatYouHaveLost.Repository.Data;

public class NewsData
{
    public string Id { get; set; }
    public string Content { get; set; }
    public string Image { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Title { get; set; }
    public string Font { get; set; }
}