using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visa.Core.Models;
using Visa.Infrastructure.Dtos.Article;
using Visa.Infrastructure.Filters;

namespace Visa.Infrastructure.Repositories
{
    public class ArticlesRepository : BaseRepository<Article, MyDbContext>
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public ArticlesRepository(MyDbContext context,IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<Article> FilterArticles(int? categoryId, string searchString = null)
        {
            IEnumerable<Article> filteredData;
            var articles = _context.Articles.Include(a => a.User).Include(a => a.ArticleTags);

            if (categoryId != null && searchString != null)
                filteredData = articles.Where(a => a.ArticleCategoryId == categoryId && (a.Title.ToLower().Contains(searchString) || a.Description.ToLower().Contains(searchString.ToLower()) || a.ArticleTags.Select(t => t.Title).Contains(searchString.ToLower())));
            else if (categoryId != null)
                filteredData = articles.Where(a => a.ArticleCategoryId == categoryId);
            else if (searchString != null)
                filteredData = articles.Where(a => a.Title.ToLower().Contains(searchString) || a.Description.ToLower().Contains(searchString.ToLower()) || a.ArticleTags.Select(t => t.Title).Contains(searchString.ToLower()));
            else
                filteredData = articles;

            return filteredData;
        }
        public int GetArticlesCount(int? categoryId,string searchString = null)
        {
            return FilterArticles(categoryId,searchString).Count();
        }
        public List<ArticleListDto> GetArticlesList(PaginationFilter pagination,int? categoryId, string searchString = null)
        {
           var articlesList = FilterArticles(categoryId,searchString);

            articlesList = articlesList.Skip((pagination.PageNumber - 1) * pagination.PageSize)
               .Take(pagination.PageSize).ToList();
            var articlesListDto = _mapper.Map<List<ArticleListDto>>(articlesList);
            return articlesListDto;
        }
        public async Task<Article> GetArticle(int id)
        {
            var article = await _context.Articles.Include(p=>p.User).Include(p=>p.ArticleTags).Include(a=>a.ArticleCategory).Include(a=>a.ArticleHeadLines).FirstOrDefaultAsync(p=>p.Id == id);
            if (article == null)
                return null;

            article.ViewCount += 1;
            await Update(article);

            return article;
        }
        public async Task<ArticleDetailDto> GetArticleDetailed(int id)
        {
            var article = await _context.Articles.Include(p => p.User).Include(p => p.ArticleTags).Include(a => a.ArticleCategory).Include(a => a.ArticleHeadLines).Include(a=>a.ArticleComments).FirstOrDefaultAsync(p => p.Id == id);
            if (article == null)
                return null;

            article.ViewCount += 1;
            await Update(article);
            var dto =_mapper.Map<ArticleDetailDto>(article);
            return dto;
        }
        public async Task<ArticleEditDto> UpdateArticle(ArticleEditDto model)
        {
            var article = await _context.Articles.Include(p => p.User).Include(p => p.ArticleTags).Include(a=>a.ArticleHeadLines).FirstOrDefaultAsync(p => p.Id == model.Id);
            if (article == null)
                return null;
            article.Title = model.Title;
            article.Description = model.Description;
            article.ArticleCategoryId = model.ArticleCategoryId;

            await Update(article);

            _context.ArticleTags.RemoveRange(article.ArticleTags);
            _context.ArticleTags.AddRange(_mapper.Map<List<ArticleTag>>(model.ArticleTags));

            _context.ArticleHeadLines.RemoveRange(article.ArticleHeadLines);
            _context.ArticleHeadLines.AddRange(_mapper.Map<List<ArticleHeadLine>>(model.ArticleHeadLines));

            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<Article> DeleteArticle(int id)
        {
            var article = await _context.Articles.Include(a=>a.ArticleHeadLines).Include(a=>a.ArticleComments).Include(a=>a.ArticleTags).FirstOrDefaultAsync(a=>a.Id == id);
            if (article == null)
                return article;
            _context.ArticleHeadLines.RemoveRange(article.ArticleHeadLines);
            _context.ArticleTags.RemoveRange(article.ArticleTags);
            _context.ArticleComments.RemoveRange(article.ArticleComments);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return article;
        }
        public async Task<List<ArticleCommentsDto>> GetComments(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
                return null;
            var comments = await _context.ArticleComments.Where(c => c.ArticleId == id).ToListAsync();
            var commentsDto = _mapper.Map<List<ArticleCommentsDto>>(comments);
            return commentsDto;
        }
        public async Task<ArticleCommentsDto> GetComment(int id,int commentId)
        {
            var comment = await _context.ArticleComments.Where(c => c.ArticleId == id && c.Id == commentId).FirstOrDefaultAsync();
            if (comment == null)
                return null;
            var commentDto = _mapper.Map<ArticleCommentsDto>(comment);
            return commentDto;
        }
        public async Task<ArticleCommentsDto> DeleteComment(int id, int commentId)
        {
            var comment = await _context.ArticleComments.Where(c => c.ArticleId == id && c.Id == commentId).FirstOrDefaultAsync();
            if (comment == null)
                return null;
            _context.ArticleComments.RemoveRange(_context.ArticleComments.Where(c => c.ParentId == comment.Id));
            _context.ArticleComments.Remove(comment);
            await _context.SaveChangesAsync();
            var commentDto = _mapper.Map<ArticleCommentsDto>(comment);
            return commentDto;
        }
        public async Task<ArticleCommentsDto> AddComment(ArticleCommentsDto commentDto)
        {
            var article = await _context.Articles.FindAsync(commentDto.ArticleId);
            if (article == null)
                return null;
            var comment = _mapper.Map<ArticleComment>(commentDto);
            _context.ArticleComments.Add(comment);
            await _context.SaveChangesAsync();
            return _mapper.Map<ArticleCommentsDto>(comment);
        }
        public async Task<ArticleDetailDto> UploadArticleImage(int id, IFormFile file)
        {
            var article = await _context.Articles.FindAsync(id);

            if (article.Image != null)
                if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Images", "Article", article.Image)))
                    File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images", "Article", article.Image));

            var imageName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", "Article", imageName);
            using var stream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(stream);

            article.Image = imageName;
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
            var articleDto = _mapper.Map<ArticleDetailDto>(article);

            return articleDto;
        }
    }
}
