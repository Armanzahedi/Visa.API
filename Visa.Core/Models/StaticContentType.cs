using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Visa.Core.Models
{
    public class StaticContentType : IBaseEntity
    {
        public int Id { get; set; }
        [MaxLength(600)]
        public string Name { get; set; }
        [MaxLength(600)]
        public string LocalName { get; set; }
        public ICollection<StaticContentDetail> StaticContentDetails { get; set; }
    }
}
