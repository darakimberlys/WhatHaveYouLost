using Microsoft.EntityFrameworkCore;
using WhatYouHaveLost.Repository.Data;
namespace WhatYouHaveLost.Repository;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Task> Tasks { get; set; }
    public DbSet<News> NewsData { get; set; }

}