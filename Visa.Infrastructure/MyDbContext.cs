using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Visa.Core.Models;

namespace Visa.Infrastructure
{
    public class MyDbContext : IdentityDbContext<User>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public DbSet<ArticleHeadLine> ArticleHeadLines { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
        public DbSet<StaticContentType> StaticContentTypes { get; set; }
        public DbSet<StaticContentDetail> StaticContentDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
