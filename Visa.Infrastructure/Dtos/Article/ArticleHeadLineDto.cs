using System;
using System.Collections.Generic;
using System.Text;

namespace Visa.Infrastructure.Dtos.Article
{
    public class ArticleHeadLineDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ArticleId { get; set; }
    }
}
