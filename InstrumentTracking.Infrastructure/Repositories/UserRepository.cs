using InstrumentsTracking.Domain.Models;
using InstrumentTracking.Domain;
using InstrumentTracking.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTracking.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public UserRepository(AppDbContext context, ILogger<EngineerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public User Create(User user)
        {
            _context.Users.Add(user);
            user.ID = _context.SaveChanges();
            _logger.LogInformation("Created new user");
            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public User GetById(int id)
        {
            _logger.LogInformation("Get user by id");
            return _context.Users.FirstOrDefault(u => u.ID == id);
        }

        public User GetByLogin(string login)
        {
            _logger.LogInformation("Get user by login");
            return _context.Users.FirstOrDefault(u => u.Login == login);
        }

        public async Task<Exceptions> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                _logger.LogError("Cant delete user {DT}", DateTime.UtcNow.ToLongTimeString());
                return Exceptions.NotFound;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Delete user {DT}", DateTime.UtcNow.ToLongTimeString());
            return Exceptions.Ok;
        }


        public async Task<Exceptions> UpdateAsync(User user, int id)
        {
            if (id != user.ID)
            {
                _logger.LogError("Cant put user {DT}", DateTime.UtcNow.ToLongTimeString());

                return Exceptions.BadRequest;
            }

            _context.Entry(user).State = EntityState.Modified;

            _logger.LogInformation("Update user {DT}", DateTime.UtcNow.ToLongTimeString());

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return Exceptions.NotFound;
                }
                else
                {
                    throw;
                }
            }

            return Exceptions.Ok;
        }



        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
