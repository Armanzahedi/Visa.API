using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Visa.Core.Models
{
    public class ArticleComment
    {
        public int Id { get; set; }
        [MaxLength(300)]
        public string Name { get; set; }
        [MaxLength(400)]
        public string Email { get; set; }
        [MaxLength(800)]
        public string Message { get; set; }
        public DateTime? AddedDate { get; set; }
        public int? ParentId { get; set; }
        public virtual ArticleComment Parent { get; set; }
        public virtual ICollection<ArticleComment> Children { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }

    }
}
