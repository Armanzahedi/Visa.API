using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Visa.Infrastructure.Dtos.Partner;
using Visa.Infrastructure.Filters;
using Visa.Infrastructure.Helpers;
using Visa.Infrastructure.Repositories;
using Visa.Infrastructure.Services;
using Visa.Infrastructure.Wrappers;

namespace Visa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : BaseController
    {
        private readonly PartnersRepository _repo;
        private readonly IMapper _mapper;
        public PartnersController(PartnersRepository repository, IUriService uriService, IMapper mapper) : base(uriService)
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

            var partners = await _repo.GetSome(pagination);
            if (partners == null || partners.Any() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<List<PartnerListDto>>() { Succeeded = false, Message = "محتوا ای یافته نشد" });

            var dto = _mapper.Map<List<PartnerListDto>>(partners);
            var pagedReponse = PaginationHelper.CreatePagedReponse(dto, pagination, totalRecords, uriService, route);
            return Ok(pagedReponse);
        }
    }
}
