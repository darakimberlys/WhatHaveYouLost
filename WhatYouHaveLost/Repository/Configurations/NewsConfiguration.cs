using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhatYouHaveLost.Repository.Data;

namespace WhatYouHaveLost.Repository.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.ToTable("News"); 

            builder.HasKey(n => n.Id);
            
            builder.Property(n => n.Content).HasColumnName("Content").IsRequired();
            builder.Property(n => n.Image).HasColumnName("Image").IsRequired();
            builder.Property(n => n.PublishDate).HasColumnName("PublishDate").HasColumnType("datetime").IsRequired();
            builder.Property(n => n.Title).HasColumnName("Title").HasMaxLength(255).IsRequired();
            builder.Property(n => n.Author).HasColumnName("Author").IsRequired();
        }
    }
}