using InstrumentsTracking.Domain.Models;
using InstrumentTracking.Domain;
using InstrumentTracking.Domain.Dtos;
using InstrumentTracking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTracking.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Create(User user)
        {
            return _userRepository.Create(user);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<Exceptions> DeleteAsync(int id)
        {
            var result = await _userRepository.DeleteAsync(id);
            return result;
        }

        public async Task<Exceptions> UpdateAsync(User entity, int id)
        {
            var result = await _userRepository.UpdateAsync(entity, id);
            return result;
        }


        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public User GetByLogin(string login)
        {
            return _userRepository.GetByLogin(login);
        }
    }
}
