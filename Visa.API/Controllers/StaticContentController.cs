using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Visa.Core.Models;
using Visa.Infrastructure.Dtos.StaticContent;
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

        //[HttpGet]
        //[Route("{id:int?}")]
        //[Route("{identifier}")]
        //public async Task<IActionResult> Get(int? id,string identifier)
        //{
        //    StaticContentType contentType;
        //    if (id != null)
        //        contentType = await _repo.Get(id.Value);
        //    else
        //        contentType = await _repo.Get(identifier);

        //    if (contentType == null)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response<StaticContentType>() { Succeeded = false, Message = "محتوا پیدا نشد" });


        //    return Ok(new Response<StaticContentType>(contentType));
        //}
        [HttpGet]
        [Route("{id:int?}")]
        [Route("{identifier}")]
        public async Task<IActionResult> GetContentDetailsList(int? id,string identifier)
        {
            List<ContentDetailDto> contentDetails;
            if (id != null)
                contentDetails = await _repo.GetContentDetailsList(id.Value);
            else
                contentDetails = await _repo.GetContentDetailsList(identifier);

            if (contentDetails == null || !contentDetails.Any())
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<List<ContentDetailDto>>() { Succeeded = false, Message = "محتوا پیدا نشد" });


            return Ok(new Response<List<ContentDetailDto>>(contentDetails));
        }
        [HttpGet]
        [Route("ContentDetails/{id:int?}")]
        [Route("{typeId:int?}/ContentDetails/{identifier}")]
        [Route("{typeIdentifier}/ContentDetails/{identifier}")]
        public async Task<IActionResult> GetContentDetail(int? typeId,string typeIdentifier,int? id, string identifier)
        {
            ContentDetailDto contentDetail;
            if (id != null)
                contentDetail = await _repo.GetContentDetail(id.Value);
            else
            {
                if(typeId != null)
                    contentDetail = await _repo.GetContentDetail(typeId.Value, identifier);
                else
                    contentDetail = await _repo.GetContentDetail(typeIdentifier, identifier);
            }

            if (contentDetail == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ContentDetailDto>() { Succeeded = false, Message = "محتوا پیدا نشد" });


            return Ok(new Response<ContentDetailDto>(contentDetail));
        }
        [HttpPost]
        public async Task<IActionResult> Create(StaticContentType model)
        {
            var result = await _repo.Add(model);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<StaticContentType>() { Succeeded = false, Message = "ثبت محتوا با مشکل مواجه شد لطفا ورودی های خود را چک کرده و مجددا تلاش کنید" });


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
