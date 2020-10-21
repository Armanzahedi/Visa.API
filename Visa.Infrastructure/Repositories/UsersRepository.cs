using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Visa.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visa.Infrastructure.Dtos;
using Visa.Infrastructure.Helpers;
using Visa.Infrastructure.Filters;
using Visa.Infrastructure.Dtos.User;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Visa.Infrastructure.Repositories
{
    public interface IUsersRepository
    {
        Task<UserDto> GetUser(string id);
        Task<List<UserDto>> GetUsers(PaginationFilter pagination, string searchString);
        IQueryable<User> FilterUsers(string searchString = null);
        Task<UserDto> CreateUser(UserCreateDto model);
        Task<UserDto> UpdateUser(string id, UserEditDto newUser);
        Task<IdentityResult> DeleteUser(string id);
        Task AddUserRole(User user, bool isAdmin);
        Task<bool> UserNameExists(string username, string id = null);
        Task<bool> EmailExists(string email, string id = null);
        Task<UserDto> UploadUserImage(string id, IFormFile file);

    }
    public class UsersRepository : IUsersRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        public readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UsersRepository(MyDbContext context, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IQueryable<User> FilterUsers(string searchString = null)
        {
            IQueryable<User> users = null;

            if (searchString != null)
                users = _userManager.Users.Where(u => u.UserName.ToLower().Contains(searchString.ToLower()) || u.Email.ToLower().Contains(searchString.ToLower()));
            else
                users = _userManager.Users;

            return users;
        }
        public async Task<List<UserDto>> GetUsers(PaginationFilter pagination, string searchString = null)
        {
            var usersList = new List<UserDto>();

            var users = FilterUsers(searchString);

            users = users.Skip((pagination.PageNumber - 1) * pagination.PageSize)
               .Take(pagination.PageSize);

            var usersFiltered = await users.ToListAsync();

            foreach (var user in usersFiltered)
            {
                var userDto = _mapper.Map<UserDto>(user);
                var userRoles = await _userManager.GetRolesAsync(user);

                if (userRoles.Where(r => r == "Admin").Any())
                    userDto.IsAdmin = true;
                else
                    userDto.IsAdmin = false;
                usersList.Add(userDto);
            }
            return usersList;
        }
        public async Task<UserDto> GetUser(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return null;
            var userDto = _mapper.Map<UserDto>(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Where(r => r == "Admin").Any())
                userDto.IsAdmin = true;
            else
                userDto.IsAdmin = false;
            return userDto;
        }
        public async Task<UserDto> UpdateUser(string id, UserEditDto model)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return null;

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Information = model.Information;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await AddUserRole(user, model.IsAdmin);
            }

            var userDto = _mapper.Map<UserDto>(user);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                if (userRoles.Where(r => r == "Admin").Any())
                    userDto.IsAdmin = true;
                else
                    userDto.IsAdmin = false;
            }
            return userDto;
        }
        public async Task<UserDto> CreateUser(UserCreateDto model)
        {
            User user = new User()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                await AddUserRole(user, model.IsAdmin);

            var userDto = _mapper.Map<UserDto>(user);

            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                if (userRoles.Where(r => r == "Admin").Any())
                    userDto.IsAdmin = true;
                else
                    userDto.IsAdmin = false;
            }
            return userDto;
        }
        public async Task AddUserRole(User user, bool isAdmin)
        {
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Author))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Author));

            if (isAdmin)
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                await _userManager.RemoveFromRoleAsync(user, UserRoles.Author);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Author);
                await _userManager.RemoveFromRoleAsync(user, UserRoles.Admin);
            }
        }
        public async Task<IdentityResult> DeleteUser(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return null;

            var userRoles = await _context.UserRoles.Where(r => r.UserId == user.Id).ToListAsync();
            foreach (var role in userRoles)
                _context.UserRoles.Remove(role);
            _context.SaveChanges();

            if (user.Avatar != null)
                if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Images", "UserAvatar", user.Avatar)))
                    File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images", "UserAvatar", user.Avatar));

            var result = await _userManager.DeleteAsync(user);
            return result;
        }
        public async Task<bool> UserNameExists(string username, string id = null)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                if (string.IsNullOrEmpty(id))
                {
                    if (user != null)
                        return true;
                }
                else
                {
                    if (user.Id != id)
                        return true;
                }
            }
            return false;
        }
        public async Task<bool> EmailExists(string email, string id = null)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                if (string.IsNullOrEmpty(id))
                {
                    if (user != null)
                        return true;
                }
                else
                {
                    if (user.Id != id)
                        return true;
                }
            }
            return false;
        }
        public async Task<UserDto> UploadUserImage(string id, IFormFile file)
        {
            var user = await _userManager.FindByIdAsync(id);

            if(user.Avatar != null)
                if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Images", "UserAvatar", user.Avatar)))
                    File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images", "UserAvatar", user.Avatar));

            var imageName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", "UserAvatar", imageName);
            using var stream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(stream);

            user.Avatar = imageName;
            await _userManager.UpdateAsync(user);
            var userDto = _mapper.Map<UserDto>(user);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                if (userRoles.Where(r => r == "Admin").Any())
                    userDto.IsAdmin = true;
                else
                    userDto.IsAdmin = false;
            }
            return userDto;
        }
    }
}
