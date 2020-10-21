using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Visa.Core.Models
{
    public class ArticleCategory : IBaseEntity
    {
        public int Id { get; set; }
        [MaxLength(400,ErrorMessage = "نام دسته باید از 400 کارکتر کمتر باشد")]
        public string Title { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
