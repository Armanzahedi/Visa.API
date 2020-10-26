using System;
using System.Collections.Generic;
using System.Text;

namespace Visa.Infrastructure.Dtos.Service
{
    public class ServicesListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Thumbnail { get; set; }
    }
}
