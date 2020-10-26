using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Visa.Core.Models;
using Visa.Infrastructure.Dtos.OurTeam;
using Visa.Infrastructure.Filters;
using Visa.Infrastructure.Helpers;
using Visa.Infrastructure.Repositories;
using Visa.Infrastructure.Services;
using Visa.Infrastructure.Wrappers;

namespace Visa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OurTeamController : BaseController
    {
        private readonly OurTeamsRepository _repo;
        private readonly IMapper _mapper;
        public OurTeamController(OurTeamsRepository repository, IUriService uriService,IMapper mapper) : base(uriService)
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

            var ourTeam = await _repo.GetSome(pagination);
            if (ourTeam == null || ourTeam.Any() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<List<OurTeamListDto>>() { Succeeded = false, Message = "محتوا ای یافته نشد" });

            var dto = _mapper.Map<List<OurTeamListDto>>(ourTeam);
            var pagedReponse = PaginationHelper.CreatePagedReponse(dto, pagination, totalRecords, uriService, route);
            return Ok(pagedReponse);
        }
    }
}
