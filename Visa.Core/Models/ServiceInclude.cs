using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Visa.Core.Models
{
    public class ServiceInclude
    {
        public int Id { get; set; }
        [MaxLength(700)]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public int ServcieId { get; set; }
        public Service Service { get; set; }
    }
}
