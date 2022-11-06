using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Services.IManager
{
    public interface IUserManager
    {
        Task<bool> Register(CreateUserDto user);
        Task<UserDto> Login(string usuario, string password);
        Task<List<UserGetDto>> GetAllUsers();
    }
}
