using System;
using System.Collections.Generic;
using System.Text;
using Visa.Core.Models;

namespace Visa.Infrastructure.Repositories
{
    public class PartnersRepository : BaseRepository<Partner, MyDbContext>
    {
        private readonly MyDbContext _context;
        public PartnersRepository(MyDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
