using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Visa.Core.Models;
using Visa.Infrastructure.Dtos;
using Visa.Infrastructure.Dtos.User;
using Visa.Infrastructure.Repositories;
using Visa.Infrastructure.Services;
using Visa.Infrastructure.Wrappers;

namespace Visa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo,IUriService uriService) : base(uriService)
        {
            _repo = repo;

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDto user)
        {
            var token = await _repo.Login(user);
            if (token == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<IActionResult>() { Succeeded = false, Message = "نام یا رمز عبور وارد شده صحیح نیست" });

            var userToken = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };
            return Ok(new Response<dynamic>(userToken) { Message = "توکن با موفقیت صادر شد" });
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(UserRegisterDto model)
        {
            var userExists = await _repo.UserExists(model.UserName);
            if (userExists)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>() { Succeeded = false, Message = "کاربر دیگری با همین نام در سیستم ثبت شده" });


            var emailExists = await _repo.EmailExists(model.Email);
            if (emailExists)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>() { Succeeded = false, Message = "کاربر دیگری با همین ایمیل در سیستم ثبت شده" });

            var result = await _repo.Register(model);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<User>() { Succeeded = false, Message = "بت کاربر با مشکل مواجه شد لطفا ورودی های خود را چک کرده و مجددا تلاش کنید" });


            return Ok(new Response<UserDto>(result) { Message="کاربر با موفقیت ثبت شد"});
        }
    }
}
