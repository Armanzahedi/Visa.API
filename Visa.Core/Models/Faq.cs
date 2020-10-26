using System;
using System.Collections.Generic;
using System.Text;

namespace Visa.Core.Models
{
    public class Faq : IBaseEntity
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
