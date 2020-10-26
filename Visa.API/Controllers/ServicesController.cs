using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Visa.Infrastructure.Dtos.Service;
using Visa.Infrastructure.Filters;
using Visa.Infrastructure.Helpers;
using Visa.Infrastructure.Repositories;
using Visa.Infrastructure.Services;
using Visa.Infrastructure.Wrappers;

namespace Visa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : BaseController
    {
        private readonly ServicesRepository _repo;
        private readonly IMapper _mapper;
        public ServicesController(ServicesRepository repository, IUriService uriService, IMapper mapper) : base(uriService)
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

            var servicesList = await _repo.GetSome(pagination);
            if (servicesList == null || servicesList.Any() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<List<ServicesListDto>>() { Succeeded = false, Message = "محتوا ای یافته نشد" });

            var dto = _mapper.Map<List<ServicesListDto>>(servicesList);
            var pagedReponse = PaginationHelper.CreatePagedReponse(dto, pagination, totalRecords, uriService, route);
            return Ok(pagedReponse);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var service = await _repo.GetService(id);
            if (service == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ServicesDetailDto>() { Succeeded = false, Message = "محتوا پیدا نشد" });

            var dto = _mapper.Map<ServicesDetailDto>(service);
            return Ok(new Response<ServicesDetailDto>(dto));
        }
    }
}
