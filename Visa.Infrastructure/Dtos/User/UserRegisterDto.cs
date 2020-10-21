using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Visa.Infrastructure.Dtos.User
{
    public class UserRegisterDto
    {
        [EmailAddress(ErrorMessage = "ایمیل نا معتبر.")]
        [Required(ErrorMessage = "ایمیل را وارد کنید.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "نام کاربری را وارد کنید.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "پسورد را وارد کنید.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
         ErrorMessage = "پسورد باید بیشتر از 8 کارکتر بوده و حداقل شامل یک حرف بزرگ یک حرف کوچک یک عدد و یک کارکتر خاص باشد.")]
        public string Password { get; set; }

        [MaxLength(300, ErrorMessage = "نام شما باید از 300 کارکتر کمتر باشد")]
        [Required(ErrorMessage = "نام خود را وارد کنید.")]
        public string FirstName { get; set; }

        [MaxLength(300, ErrorMessage = "نام خانوادگی شما باید از 300 کارکتر کمتر باشد")]
        [Required(ErrorMessage = "نام خانوادگی خود را وارد کنید.")]
        public string LastName { get; set; }
        public string Information { get; set; }

    }
}
