using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ArticleCategoriesController : BaseController
    {
        private readonly ArticleCategoriesRepository _repo;
        public ArticleCategoriesController(ArticleCategoriesRepository repository, IUriService uriService) : base(uriService)
        {
            _repo = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PaginationFilter paginationFilter)
        {
            var route = Request.Path.Value;
            var totalRecords = await _repo.GetCount();
            var pagination = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

            var categories = await _repo.GetSome(pagination);
            if (categories == null || categories.Any() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ArticleCategory>() { Succeeded = false, Message = "دسته ای یافته نشد" });


            var pagedReponse = PaginationHelper.CreatePagedReponse<ArticleCategory>(categories, pagination, totalRecords, uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var category = await _repo.Get(id);
            if (category == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ArticleCategory>() { Succeeded = false, Message = "دسته پیدا نشد" });


            return Ok(new Response<ArticleCategory>(category));
        }
        [HttpGet]
        [Route("{id}/Articles")]
        public async Task<IActionResult> GetCategoryArticles(int id)
        {

            var articles = await _repo.GetArticles(id);
            if (articles == null || !articles.Any())
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ArticleCategory>() { Succeeded = false, Message = "مقاله ای پیدا نشد" });


            return Ok(new Response<List<ArticleListDto>>(articles));
        }
        [HttpPost]
        public async Task<IActionResult> Create(ArticleCategory model)
        {
            var result = await _repo.Add(model);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ArticleCategory>() { Succeeded = false, Message = "ثبت دسته با مشکل مواجه شد لطفا ورودی های خود را چک کرده و مجددا تلاش کنید" });


            return Ok(new Response<ArticleCategory>(result) { Message = "دسته با موفقیت ثبت شد" });
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id,ArticleCategory model)
        {
            model.Id = id;
            var result = await _repo.Update(model);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ArticleCategory>() { Succeeded = false, Message = "ثبت دسته با مشکل مواجه شد لطفا ورودی های خود را چک کرده و مجددا تلاش کنید" });

            return Ok(new Response<ArticleCategory>(result) { Message = "دسته با موفقیت بروزرسانی شد" });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repo.Delete(id);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ArticleCategory>() { Succeeded = false, Message = "دسته پیدا نشد" });

            return Ok(new Response<ArticleCategory>(result) { Message = "دسته با موفقین حذف شد" });
        }
    }
}
