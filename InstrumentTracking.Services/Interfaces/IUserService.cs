using InstrumentsTracking.Domain.Models;
using InstrumentTracking.Domain;
using InstrumentTracking.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTracking.Services
{
    public interface IUserService
    {
        User Create(User user);
        User GetByLogin(string login);
        User GetById(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<Exceptions> DeleteAsync(int id);
        Task<Exceptions> UpdateAsync(User user, int id);
    }
}
