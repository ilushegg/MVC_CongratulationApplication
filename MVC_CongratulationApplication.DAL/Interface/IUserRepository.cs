using MVC_CongratulationApplication.Domain.Entity;

namespace MVC_CongratulationApplication.DAL.Interface
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> Edit(User entity);

        Task<User> GetFirst();


    }
}
