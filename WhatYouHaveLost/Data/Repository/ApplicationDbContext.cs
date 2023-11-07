using Microsoft.EntityFrameworkCore;
using WhatYouHaveLost.Data.Repository.Configurations;
using WhatYouHaveLost.Model.Data;

namespace WhatYouHaveLost.Data.Repository;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    public DbSet<News> News { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NewsConfiguration());
    }
}