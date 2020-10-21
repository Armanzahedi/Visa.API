using System;
using System.Collections.Generic;
using System.Text;

namespace Visa.Infrastructure.Dtos.Article
{
    public class ArticleTagDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ArticleId { get; set; }
    }
}
