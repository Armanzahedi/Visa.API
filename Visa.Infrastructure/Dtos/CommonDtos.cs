using System;
using System.Collections.Generic;
using System.Text;

namespace Visa.Infrastructure.Dtos
{
    public class PaginationDto
    {
        public int TotalPageCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
