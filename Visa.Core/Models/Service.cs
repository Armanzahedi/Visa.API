using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Visa.Core.Models
{
    public class Service : IBaseEntity
    {
        public int Id { get; set; }
        [MaxLength(600)]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [DataType(DataType.MultilineText)]
        public string Phone { get; set; }
        [DataType(DataType.MultilineText)]
        public string Email { get; set; }
        [DataType(DataType.MultilineText)]
        public string FileInfo { get; set; }
        public string File { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public ICollection<ServiceInclude> ServiceIncludes { get; set; }
    }
}
