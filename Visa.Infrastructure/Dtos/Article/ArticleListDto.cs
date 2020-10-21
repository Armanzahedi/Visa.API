using System;
using System.Collections.Generic;
using System.Text;
using Visa.Core.Models;

namespace Visa.Infrastructure.Dtos.Article
{
    public class ArticleListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ArticleTagDto> ArticleTags { get; set; }
        public DateTime AddedDate { get; set; }
        public string Image { get; set; }
        public ArticleAuthorDto Author { get; set; }
    }
}
