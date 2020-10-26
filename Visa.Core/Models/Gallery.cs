﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Visa.Core.Models
{
    public class Gallery : IBaseEntity
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
    }
}
