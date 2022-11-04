using Dulce.Heladeria.Services.Dtos;
using Dulce.Heladeria.Services.IManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dulce.Heladeria.Repositories.IRepositories;
using AutoMapper;
using Dulce.Heladeria.Models.Entities;
using Dulce.Heladeria.Models.UnitOfWork;
using Dulce.Heladeria.Repositories.Repositories;
using System.Linq;

namespace Dulce.Heladeria.Services.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserManager(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<bool> Register(CreateUserDto user)
        {
            if(await _userRepository.ExistsUser(user.Email))
            {
                throw new InvalidOperationException("El usuario ya se encuentra registrado");
            }

            byte[] passwordHash, passwordSalt;

            PasswordHashBuilding(user.Password, out passwordHash, out passwordSalt);

            var userEntity = _mapper.Map<UserEntity>(user);

            userEntity.PasswordHash = passwordHash;
            userEntity.PasswordSalt = passwordSalt;

            await _userRepository.InsertAsync(userEntity);
            var result = await _unitOfWork.SaveChangesAsync();
            return result >= 0;  
        }

        public async Task<UserDto> Login(string userEmail, string password)
        {
            var userEntity = await _userRepository.GetAsync(x => x.Equals(userEmail));
            var user = userEntity.FirstOrDefault();

            if (user is null)
            {
                return null;
            }

            if (!PasswordHashValidation(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return new UserDto() { Id = user.Id, Email = user.Email };
        }
        private bool PasswordHashValidation(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var hashComputado = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < hashComputado.Length; i++)
                {
                    if (hashComputado[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }
        private void PasswordHashBuilding(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
