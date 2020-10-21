using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visa.Core.Models;

namespace Visa.Infrastructure.Repositories
{
   public class StaticContentsRepository : BaseRepository<StaticContentType, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        public StaticContentsRepository(MyDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<StaticContentType> DeleteContentType(int id)
        {
            var contentType = await _context.StaticContentTypes.Include(a => a.StaticContentDetails).FirstOrDefaultAsync(a => a.Id == id);
            if (contentType == null)
                return contentType;

            _context.StaticContentDetails.RemoveRange(contentType.StaticContentDetails);
            _context.StaticContentTypes.Remove(contentType);
            await _context.SaveChangesAsync();
            return contentType;
        }
        public async Task<List<StaticContentDetail>> GetContentDetails(int contentTypeId)
        {
            var contentDetails = await _context.StaticContentDetails.Where(c=>c.StaticContentTypeId == contentTypeId).ToListAsync();
            return contentDetails;
        }
        public async Task<List<StaticContentDetail>> GetContentDetails(string name)
        {
            var contentDetails = await _context.StaticContentDetails.Where(c => c.StaticContentType.Name.ToLower().Equals(name.ToLower())).ToListAsync();
            return contentDetails;
        }
        public async Task<StaticContentDetail> DeleteContentDetail(int typeId,int id)
        {
            var contentDetail = await _context.StaticContentDetails.FirstOrDefaultAsync(c=>c.StaticContentTypeId == typeId && c.Id == id);
            if (contentDetail == null)
                return null;
            _context.StaticContentDetails.Remove(contentDetail);
            await _context.SaveChangesAsync();
            return contentDetail;
        }
        public async Task<StaticContentDetail> AddContentDetail(StaticContentDetail model)
        {
            _context.StaticContentDetails.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<StaticContentDetail> UpdateContentDetail(StaticContentDetail model)
        {
            var contentDetail = await _context.StaticContentDetails.FindAsync(model.Id);
            if (contentDetail == null)
                return null;

            _context.Entry(contentDetail).State = EntityState.Detached;
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<StaticContentDetail> UploadContentImage(int id, IFormFile file)
        {
            var contentDetail = await _context.StaticContentDetails.FindAsync(id);

            if (contentDetail.Image != null)
                if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Images", "Content", contentDetail.Image)))
                    File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images", "Content", contentDetail.Image));

            var imageName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", "Content", imageName);
            using var stream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(stream);

            contentDetail.Image = imageName;
            _context.StaticContentDetails.Update(contentDetail);
            await _context.SaveChangesAsync();

            return contentDetail;
        }
    }
}
