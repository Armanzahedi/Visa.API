using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Visa.Core.Models
{
    public class Article : IBaseEntity
    {
        public int Id { get; set; }
        [MaxLength(600)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public int ViewCount { get; set; }
        public string Image { get; set; }
        public DateTime? AddedDate { get; set; }

        public int? ArticleCategoryId { get; set; }
        public ArticleCategory ArticleCategory { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<ArticleHeadLine> ArticleHeadLines { get; set; }
        public ICollection<ArticleTag> ArticleTags { get; set; }
        public ICollection<ArticleComment> ArticleComments { get; set; }
    }
}
