using Microsoft.EntityFrameworkCore;
using WhatYouHaveLost.Repository.Data;
namespace WhatYouHaveLost.Repository;

public class ApplicationDbContext : DbContext
{
    public DbSet<NewsData> NewsData { get; set; }
}
    