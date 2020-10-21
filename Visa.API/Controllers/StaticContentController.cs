using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Visa.Core.Models;
using Visa.Infrastructure.Filters;
using Visa.Infrastructure.Helpers;
using Visa.Infrastructure.Repositories;
using Visa.Infrastructure.Services;
using Visa.Infrastructure.Wrappers;

namespace Visa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaticContentController : BaseController
    {
        private readonly StaticContentsRepository _repo;
        public StaticContentController(StaticContentsRepository repository, IUriService uriService) : base(uriService)
        {
            _repo = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PaginationFilter paginationFilter)
        {
            var route = Request.Path.Value;
            var totalRecords = await _repo.GetCount();
            var pagination = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

            var contentTypes = await _repo.GetSome(pagination);
            if (contentTypes == null || contentTypes.Any() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<StaticContentType>() { Succeeded = false, Message = "محتوا ای یافته نشد" });


            var pagedReponse = PaginationHelper.CreatePagedReponse<StaticContentType>(contentTypes, pagination, totalRecords, uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var contentType = await _repo.Get(id);
            if (contentType == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ArticleCategory>() { Succeeded = false, Message = "محتوا پیدا نشد" });


            return Ok(new Response<StaticContentType>(contentType));
        }
        [HttpGet]
        [Route("{id}/ContentDetails")]
        public async Task<IActionResult> GetContentByContentTypeId(int id)
        {

            var contentDetails = await _repo.GetContentDetails(id);
            if (contentDetails == null || !contentDetails.Any())
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<StaticContentType>() { Succeeded = false, Message = "محتوا پیدا نشد" });


            return Ok(new Response<List<StaticContentDetail>>(contentDetails));
        }
        [HttpGet]
        [Route("ContentDetailsByContentName")]
        public async Task<IActionResult> GetContentByContentTypeName(string name)
        {

            var contentDetails = await _repo.GetContentDetails(name);
            if (contentDetails == null || !contentDetails.Any())
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<StaticContentType>() { Succeeded = false, Message = "محتوا پیدا نشد" });


            return Ok(new Response<List<StaticContentDetail>>(contentDetails));
        }
        [HttpPost]
        public async Task<IActionResult> Create(StaticContentType model)
        {
            var result = await _repo.Add(model);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ArticleCategory>() { Succeeded = false, Message = "ثبت محتوا با مشکل مواجه شد لطفا ورودی های خود را چک کرده و مجددا تلاش کنید" });


            return Ok(new Response<StaticContentType>(result) { Message = "محتوا با موفقیت ثبت شد" });
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, StaticContentType model)
        {
            model.Id = id;
            var result = await _repo.Update(model);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<StaticContentType>() { Succeeded = false, Message = "ثبت محتوا با مشکل مواجه شد لطفا ورودی های خود را چک کرده و مجددا تلاش کنید" });

            return Ok(new Response<StaticContentType>(result) { Message = "محتوا با موفقیت بروزرسانی شد" });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repo.DeleteContentType(id);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<StaticContentType>() { Succeeded = false, Message = "دسته پیدا نشد" });

            return Ok(new Response<StaticContentType>(result) { Message = "دسته با موفقین حذف شد" });
        }
        [HttpPost]
        [Route("{id}/ContentDetails")]
        public async Task<IActionResult> AddContentDetail(int id,StaticContentDetail contentDetail)
        {
            contentDetail.StaticContentTypeId = id;
            var result = await _repo.AddContentDetail(contentDetail);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<StaticContentDetail>() { Succeeded = false, Message = "محتوا پیدا نشد" });

            return Ok(new Response<StaticContentDetail>(result) { Message = "محتوا با موفقین ثبت شد" });
        }
        [HttpDelete]
        [Route("{typeId}/ContentDetails/{id}")]
        public async Task<IActionResult> DeleteContentDetail(int typeId,int id)
        {
            var result = await _repo.DeleteContentDetail(typeId,id);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<StaticContentDetail>() { Succeeded = false, Message = "محتوا پیدا نشد" });

            return Ok(new Response<StaticContentDetail>(result) { Message = "محتوا با موفقین حذف شد" });
        }
        [HttpPut]
        [Route("{typeId}/ContentDetails/{id}")]
        public async Task<IActionResult> UpdateContentDetail(int typeId,int id, StaticContentDetail contentDetail)
        {
            contentDetail.StaticContentTypeId = typeId;
            contentDetail.Id = id;
            var result = await _repo.UpdateContentDetail(contentDetail);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<StaticContentDetail>() { Succeeded = false, Message = "محتوا پیدا نشد" });

            return Ok(new Response<StaticContentDetail>(result) { Message = "محتوا با موفقین بروزرسانی شد" });
        }

        [HttpPost("{typeId}/ContentDetails/{id}/UploadImage")]
        public async Task<IActionResult> UploadImage(int typeId,int id, [FromForm] IFormFile file)
        {
            if (file == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<StaticContentDetail>() { Succeeded = false, Message = "لطفا تصویر محتوا را آپلود کنید" });

            var result = await _repo.UploadContentImage(id, file);
            return Ok(new Response<StaticContentDetail>(result) { Message = "تصویر با موفقیت آپلود شد" });
        }
    }
}
