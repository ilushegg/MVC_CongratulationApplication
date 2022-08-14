using MVC_CongratulationApplication.Domain.Entity;
using MVC_CongratulationApplication.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CongratulationApplication.Service.Interface
{
    public interface IContainer
    {
        Task<IBaseResponse<IEnumerable<Person>>> GetBirthdayPeople();

        Task<IBaseResponse<User>> GetUser();
    }
}
