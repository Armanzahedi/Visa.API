using System;
using System.Collections.Generic;
using System.Text;
using Visa.Core.Models;

namespace Visa.Infrastructure.Repositories
{
    public class TestimonialsRepository : BaseRepository<Testimonial, MyDbContext>
    {
        private readonly MyDbContext _context;
        public TestimonialsRepository(MyDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
