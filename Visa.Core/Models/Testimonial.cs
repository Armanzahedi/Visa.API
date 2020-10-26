using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Visa.Core.Models
{
    public class Testimonial : IBaseEntity
    {
        public int Id { get; set; }
        public string Speaker { get; set; }
        public string Message { get; set; }
        public int Rate { get; set; }
    }
}
