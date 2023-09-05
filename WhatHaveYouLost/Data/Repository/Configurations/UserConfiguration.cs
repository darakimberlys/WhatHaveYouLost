using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhatYouHaveLost.Model.Data;

namespace WhatYouHaveLost.Data.Repository.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserData>
{
    public void Configure(EntityTypeBuilder<UserData> builder)
    {
        builder.ToTable("Users"); 

        builder.HasKey(n => n.UserId);
            
        builder.Property(n => n.LoginName).HasColumnName("LoginName").HasMaxLength(40).IsRequired();
        builder.Property(n => n.Password).HasColumnName("PasswordHash").HasMaxLength(64).IsRequired();
        builder.Property(n => n.ID).HasColumnName("ID");
    }
}