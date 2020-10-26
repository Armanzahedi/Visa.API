using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Visa.Infrastructure.Dtos.Gallery;
using Visa.Infrastructure.Filters;
using Visa.Infrastructure.Helpers;
using Visa.Infrastructure.Repositories;
using Visa.Infrastructure.Services;
using Visa.Infrastructure.Wrappers;

namespace Visa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : BaseController
    {
        private readonly GalleriesRepository _repo;
        private readonly IMapper _mapper;
        public GalleryController(GalleriesRepository repository, IUriService uriService, IMapper mapper) : base(uriService)
        {
            _repo = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PaginationFilter paginationFilter)
        {
            var route = Request.Path.Value;
            var totalRecords = await _repo.GetCount();
            var pagination = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

            var galleryList = await _repo.GetSome(pagination);
            if (galleryList == null || galleryList.Any() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<List<GalleryDto>>() { Succeeded = false, Message = "محتوا ای یافته نشد" });

            var dto = _mapper.Map<List<GalleryDto>>(galleryList);
            var pagedReponse = PaginationHelper.CreatePagedReponse(dto, pagination, totalRecords, uriService, route);
            return Ok(pagedReponse);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var galleryImage = await _repo.Get(id);
            if (galleryImage == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<GalleryDto>() { Succeeded = false, Message = "محتوا پیدا نشد" });

            var dto = _mapper.Map<GalleryDto>(galleryImage);
            return Ok(new Response<GalleryDto>(dto));
        }
    }
}
