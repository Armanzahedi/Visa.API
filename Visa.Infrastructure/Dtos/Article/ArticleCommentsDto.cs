using System;
using System.Collections.Generic;
using System.Text;

namespace Visa.Infrastructure.Dtos.Article
{
    public class ArticleCommentsDto
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime? AddedDate { get; set; }
        public int? ParentId { get; set; }
        public virtual ArticleCommentsDto Parent { get; set; }
        public virtual List<ArticleCommentsDto> Children { get; set; }
    }
}
