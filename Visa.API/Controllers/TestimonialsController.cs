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
    public class TestimonialsController : BaseController
    {
        private readonly TestimonialsRepository _repo;
        public TestimonialsController(TestimonialsRepository repository, IUriService uriService) : base(uriService)
        {
            _repo = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PaginationFilter paginationFilter)
        {
            var route = Request.Path.Value;
            var totalRecords = await _repo.GetCount();
            var pagination = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

            var testimonials = await _repo.GetSome(pagination);
            if (testimonials == null || testimonials.Any() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<List<Testimonial>>() { Succeeded = false, Message = "محتوا ای یافته نشد" });

            var pagedReponse = PaginationHelper.CreatePagedReponse(testimonials, pagination, totalRecords, uriService, route);
            return Ok(pagedReponse);
        }
    }
}
