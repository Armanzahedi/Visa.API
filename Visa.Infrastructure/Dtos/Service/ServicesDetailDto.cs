using System;
using System.Collections.Generic;
using System.Text;
using Visa.Core.Models;

namespace Visa.Infrastructure.Dtos.Service
{
    public class ServicesDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FileInfo { get; set; }
        public string File { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public List<ServiceIncludesDto> ServiceIncludes { get; set; }
    }
}
