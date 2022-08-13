using MVC_CongratulationApplication.DAL.Data;
using MVC_CongratulationApplication.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CongratulationApplication.DAL.Repository
{
    public class UserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IQueryable<User> GetAll()
        {
            return _dataContext.Users;
        }

        public async Task Delete(User entity)
        {
            _dataContext.Users.Remove(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task Create(User entity)
        {
            await _dataContext.Users.AddAsync(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<User> Update(User entity)
        {
            _dataContext.Users.Update(entity);
            await _dataContext.SaveChangesAsync();

            return entity;
        }
    }
}
