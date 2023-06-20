using System;

namespace WhatYouHaveLost.Repository.Data;

public class NewsData
{
    public string NewsName { get; set; }
    public string Content { get; set; }
    public string Image { get; set; }

    public DateTime CreatedDate { get; set; }
    public string Title { get; set; }
    public string Font { get; set; }
}