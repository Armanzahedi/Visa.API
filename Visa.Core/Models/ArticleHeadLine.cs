using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Visa.Core.Models
{
    public class ArticleHeadLine
    {
        public int Id { get; set; }
        [MaxLength(700)]
        public string Title { get; set; }
        public string Description { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
