using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Visa.Core.Models;

namespace Visa.Infrastructure.Dtos.Article
{
    public class ArticleDetailDto
    {
        public int Id { get; set; }
        public DateTime AddedDate { get; set; }
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public int? ArticleCategoryId { get; set; }
        public string Image { get; set; }
        public ArticleAuthorDto Author { get; set; }
        public List<ArticleHeadLineDto> ArticleHeadLines { get; set; }
        public List<ArticleTagDto> ArticleTags { get; set; }
        public List<ArticleCommentsDto> ArticleComments { get; set; }
    }
}
