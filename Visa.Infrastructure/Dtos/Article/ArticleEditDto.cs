using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Visa.Core.Models;

namespace Visa.Infrastructure.Dtos.Article
{
    public class ArticleEditDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public int? ArticleCategoryId { get; set; }
        public List<ArticleHeadLineDto> ArticleHeadLines { get; set; }
        public List<ArticleTagDto> ArticleTags { get; set; }
        public void SetId(int id)
        {
            this.Id = id;

            foreach (var tag in ArticleTags)
                tag.ArticleId = this.Id;

            foreach (var headLine in ArticleHeadLines)
                headLine.ArticleId = this.Id;
        }
    }
}
