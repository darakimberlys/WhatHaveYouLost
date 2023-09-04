using Microsoft.EntityFrameworkCore;
using WhatYouHaveLost.Repository.Configurations;
using WhatYouHaveLost.Repository.Data;
namespace WhatYouHaveLost.Repository;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<News> News { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NewsConfiguration());
    }

}