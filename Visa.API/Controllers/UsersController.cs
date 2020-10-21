using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Visa.Core.Models;
using Visa.Infrastructure.Dtos;
using Visa.Infrastructure.Dtos.User;
using Visa.Infrastructure.Filters;
using Visa.Infrastructure.Helpers;
using Visa.Infrastructure.Repositories;
using Visa.Infrastructure.Services;
using Visa.Infrastructure.Wrappers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Visa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUsersRepository _repo;
        public UsersController(IUsersRepository repository, IUriService uriService) : base(uriService)
        {
            _repo = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PaginationFilter paginationFilter, string searchString = null)
        {
            var route = Request.Path.Value;
            var totalRecords = _repo.FilterUsers(searchString).Count();
            var pagination = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

            var users = await _repo.GetUsers(pagination, searchString);
            if (users == null || users.Any() == false)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>() { Succeeded = false, Message = "کاربری یافته نشد" });


            var pagedReponse = PaginationHelper.CreatePagedReponse<UserDto>(users, pagination, totalRecords, uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Get(string id)
        {

            var user = await _repo.GetUser(id);
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<UserDto>(){Succeeded = false, Message = "کاربر پیدا نشد" });


            return Ok(new Response<UserDto>(user));
        }
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> CreateUser(UserCreateDto model)
        {
            var userNameExists = await _repo.UserNameExists(model.UserName);
            if (userNameExists)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>() { Succeeded = false, Message = "کاربر دیگری با همین نام در سیستم ثبت شده" });


            var emailExists = await _repo.EmailExists(model.Email);
            if (emailExists)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>() { Succeeded = false, Message = "کاربر دیگری با همین ایمیل در سیستم ثبت شده" });

            var result = await _repo.CreateUser(model);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>() { Succeeded = false, Message = "بت کاربر با مشکل مواجه شد لطفا ورودی های خود را چک کرده و مجددا تلاش کنید" });


            return Ok(new Response<UserDto>(result) { Message = "کاربر با موفقیت ثبت شد" });
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UserEditDto model)
        {
            var userNameExists = await _repo.UserNameExists(model.UserName, id);
            if (userNameExists)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>() { Succeeded = false, Message = "کاربر دیگری با همین نام در سیستم ثبت شده" });

            var emailExists = await _repo.EmailExists(model.Email, id);
            if (emailExists)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>() { Succeeded = false, Message = "کاربر دیگری با همین ایمیل در سیستم ثبت شده" });

            var result = await _repo.UpdateUser(id, model);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>() { Succeeded = false, Message = "بت کاربر با مشکل مواجه شد لطفا ورودی های خود را چک کرده و مجددا تلاش کنید" });

            return Ok(new Response<UserDto>(result) { Message = "کاربر با موفقیت بروزرسانی شد" });
        }

        [HttpPost("{id}/UploadImage")]
        public async Task<IActionResult> UploadImage(string id,[FromForm] IFormFile file)
        {
            if (file == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>() { Succeeded = false, Message = "لطفا تصویر کاربر را آپلود کنید" });
            if(await _repo.GetUser(id) == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>() { Succeeded = false, Message = "کاربر پیدا نشد" });

            var result = await _repo.UploadUserImage(id, file);
            return Ok(new Response<UserDto>(result) { Message = "تصویر با موفقیت آپلود شد" });
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _repo.DeleteUser(id);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>() { Succeeded = false, Message = "کاربر پیدا نشد" });

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>() { Succeeded = false, Message = "حذف کاربر با مشکل مواجه شد" });

            return Ok(new Response<IdentityResult>(result) { Message = "کاربر با موفقین حذف شد"});
        }
    }
}
