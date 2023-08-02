using InstrumentsTracking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTracking.Domain.Interfaces
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetByLogin(string login);
        User GetById(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<Exceptions> DeleteAsync(int id);
        Task<Exceptions> UpdateAsync(User user, int id);
    }
}
