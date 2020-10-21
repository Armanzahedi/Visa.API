using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visa.Core.Models;
using Visa.Infrastructure.Dtos.Article;
using Visa.Infrastructure.Filters;

namespace Visa.Infrastructure.Repositories
{
    public class ArticleCategoriesRepository : BaseRepository<ArticleCategory, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        public ArticleCategoriesRepository(MyDbContext context,IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ArticleListDto>> GetArticles(int categoryId)
        {
            var articles = await _context.Articles.Where(a => a.ArticleCategoryId == categoryId).Include(a => a.ArticleTags).Include(a => a.User).ToListAsync();
            var articleListDto = _mapper.Map<List<ArticleListDto>>(articles);
            return articleListDto;
        }
    }
}
