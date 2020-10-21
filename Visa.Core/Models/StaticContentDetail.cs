using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Visa.Core.Models
{
   public class StaticContentDetail : IBaseEntity
    {
        public int Id { get; set; }
        [MaxLength(600)]
        public string Title { get; set; }
        public string FieldDescription { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }

        public int StaticContentTypeId { get; set; }
        public StaticContentType StaticContentType { get; set; }
    }
}
