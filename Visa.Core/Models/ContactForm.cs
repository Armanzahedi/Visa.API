using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Visa.Core.Models
{
    public class ContactForm : IBaseEntity
    {
        public int Id { get; set; }
        [MaxLength(600)]
        public string Name { get; set; }
        [MaxLength(600)]
        public string Phone { get; set; }
        [MaxLength(600)]
        public string Email { get; set; }
        public string Message { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
