using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WhatYouHaveLost.Model.Data;

namespace WhatYouHaveLost.Data.Repository;

public class AuthDbContext : IdentityDbContext<UserData>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<UserData> User { get; set; }
}