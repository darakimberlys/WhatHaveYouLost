namespace WhatYouHaveLost.Repository.Data;

public class News
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string Image { get; set; }
    public DateTime PublishDate { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
}