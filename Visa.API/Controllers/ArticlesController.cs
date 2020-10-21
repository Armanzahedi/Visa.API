using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Visa.Core.Models;
using Visa.Infrastructure.Dtos.Article;
using Visa.Infrastructure.Filters;
using Visa.Infrastructure.Helpers;
using Visa.Infrastructure.Repositories;
using Visa.Infrastructure.Services;
using Visa.Infrastructure.Wrappers;

namespace Visa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : BaseController
    {
        private readonly ArticlesRepository _repo;
        private readonly IMapper _mapper;
        public ArticlesController(ArticlesRepository repository, IUriService uriService, IMapper mapper) : base(uriService)
        {
            _repo = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PaginationFilter paginationFilter, int? categoryId,string searchString = null)
        {
            var route = Request.Path.Value;
            var totalRecords = _repo.GetArticlesCount(categoryId, searchString);
            var pagination = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

            var articles =  _repo.GetArticlesList(pagination,categoryId,searchString);
            if (articles == null || articles.Any() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<Article>() { Succeeded = false, Message = "مقاله ای یافته نشد" });


            var pagedReponse = PaginationHelper.CreatePagedReponse<ArticleListDto>(articles, pagination, totalRecords, uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var article = await _repo.GetArticleDetailed(id);
            if (article == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ArticleDetailDto>() { Succeeded = false, Message = "مقاله پیدا نشد" });

            return Ok(new Response<ArticleDetailDto>(article));
        }
        [HttpPost]
        public async Task<IActionResult> Create(Article model)
        {
            model.AddedDate = DateTime.Now;
            var result = await _repo.Add(model);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<Article>() { Succeeded = false, Message = "ثبت مقاله= با مشکل مواجه شد لطفا ورودی های خود را چک کرده و مجددا تلاش کنید" });


            return Ok(new Response<Article>(result) { Message = "مقاله با موفقیت ثبت شد" });
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, ArticleEditDto model)
        {
            model.SetId(id);
            var result = await _repo.UpdateArticle(model);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ArticleEditDto>() { Succeeded = false, Message = "ثبت مقاله با مشکل مواجه شد لطفا ورودی های خود را چک کرده و مجددا تلاش کنید" });

            return Ok(new Response<ArticleEditDto>(result) { Message = "مقاله با موفقیت بروزرسانی شد" });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repo.DeleteArticle(id);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<Article>() { Succeeded = false, Message = "مقاله پیدا نشد" });

            return Ok(new Response<Article>(result) { Message = "مقاله با موفقین حذف شد" });
        }

        [HttpPost("{id}/UploadImage")]
        public async Task<IActionResult> UploadImage(int id, [FromForm] IFormFile file)
        {
            if (file == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ArticleDetailDto>() { Succeeded = false, Message = "لطفا تصویر مقاله را آپلود کنید" });
            if (await _repo.Get(id) == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ArticleDetailDto>() { Succeeded = false, Message = "مقاله پیدا نشد" });

            var result = await _repo.UploadArticleImage(id, file);
            return Ok(new Response<ArticleDetailDto>(result) { Message = "تصویر با موفقیت آپلود شد" });
        }
        [HttpGet]
        [Route("{id}/Comments")]
        public async Task<IActionResult> GetComments(int id)
        {
            var result = await _repo.GetComments(id);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<List<ArticleCommentsDto>>() { Succeeded = false, Message = "مقاله پیدا نشد" });

            return Ok(new Response<List<ArticleCommentsDto>>(result) { Message = "" });
        }
        [HttpGet]
        [Route("{id}/Comments/{commentId}")]
        public async Task<IActionResult> GetComment(int id,int commentId)
        {
            var result = await _repo.GetComment(id,commentId);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ArticleCommentsDto>() { Succeeded = false, Message = "کامنت پیدا نشد" });

            return Ok(new Response<ArticleCommentsDto>(result) { Message = "" });
        }
        [HttpDelete]
        [Route("{id}/Comments/{commentId}")]
        public async Task<IActionResult> DeleteComment(int id, int commentId)
        {
            var result = await _repo.DeleteComment(id, commentId);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ArticleCommentsDto>() { Succeeded = false, Message = "کامنت پیدا نشد" });

            return Ok(new Response<ArticleCommentsDto>(result) { Message = "کامنت با موفقیت حذف شد" });
        }
        [HttpPost]
        [Route("{id}/Comments")]
        public async Task<IActionResult> AddComment(int id,ArticleCommentsDto comment)
        {
            comment.ArticleId = id;
            comment.AddedDate = DateTime.Now;
            var result = await _repo.AddComment(comment);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ArticleCommentsDto>() { Succeeded = false, Message = "مقاله پیدا نشد" });

            return Ok(new Response<ArticleCommentsDto>(result) { Message = "کامنت با موفقین اضافه شد" });
        }
    }
}
