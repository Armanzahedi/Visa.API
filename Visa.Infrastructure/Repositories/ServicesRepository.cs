using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Visa.Core.Models;

namespace Visa.Infrastructure.Repositories
{
    public class ServicesRepository : BaseRepository<Service, MyDbContext>
    {
        private readonly MyDbContext _context;
        public ServicesRepository(MyDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Service> GetService(int id)
        {
            var service = await _context.Services.Include(s => s.ServiceIncludes).FirstOrDefaultAsync(s => s.Id == id);
            return service;
        }
    }
}
