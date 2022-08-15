using MVC_CongratulationApplication.Domain.Entity;

namespace MVC_CongratulationApplication.DAL.Interface
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<bool> Edit(Person entity);

        Task<Person> GetByName(string name);

        Task<List<Person>> GetBirthdayPeople();
    }
}
