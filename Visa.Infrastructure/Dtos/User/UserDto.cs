using System.ComponentModel.DataAnnotations;

namespace Visa.Infrastructure.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }

        [MaxLength(300, ErrorMessage = "نام شما باید از 300 کارکتر کمتر باشد")]
        public string FirstName { get; set; }

        [MaxLength(300, ErrorMessage = "نام خانوادگی شما باید از 300 کارکتر کمتر باشد")]
        public string LastName { get; set; }
        public string Information { get; set; }

        public bool IsAdmin { get; set; } = false;
    }
}
