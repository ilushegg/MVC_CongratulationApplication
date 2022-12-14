using Microsoft.EntityFrameworkCore;
using MVC_CongratulationApplication.DAL.Data;
using MVC_CongratulationApplication.DAL.Interface;
using MVC_CongratulationApplication.Domain.Entity;

namespace MVC_CongratulationApplication.DAL.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext _dataContext;

        public PersonRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Create(Person entity)
        {
            await _dataContext.People.AddAsync(entity);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(Person entity)
        {
            _dataContext.People.Update(entity);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Person entity)
        {
            _dataContext.People.Remove(entity);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<Person> Get(int id)
        {
            return await _dataContext.People.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Person>> GetAll()
        {
            var dataContext = _dataContext.People.Include(p => p.User);
            return dataContext.ToListAsync();
        }

        public Task<List<Person>> GetBirthdayPeople()
        {
            var dataContext = _dataContext.People.Where(p => p.Birthday.Month == DateTime.Now.Month && p.Birthday.Day >= DateTime.Now.Day && p.Birthday.Day <= DateTime.Now.Day + 7);
            return dataContext.ToListAsync();
        }

    }
}
