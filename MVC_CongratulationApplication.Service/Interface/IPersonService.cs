using Microsoft.AspNetCore.Http;
using MVC_CongratulationApplication.Domain.Entity;
using MVC_CongratulationApplication.Domain.Response;
using MVC_CongratulationApplication.Domain.ViewModel;

namespace MVC_CongratulationApplication.Service.Interface
{
    public interface IPersonService
    {
        Task<IBaseResponse<IEnumerable<Person>>> GetPeople();
        Task<IBaseResponse<IEnumerable<Person>>> GetBirthdayPeople();
        Task<IBaseResponse<Person>> GetPerson(int id);

        Task<IBaseResponse<Person>> GetPersonByName(string name);

        Task<IBaseResponse<PersonViewModel>> CreatePerson(PersonViewModel model, IFormFile file);

        Task<IBaseResponse<PersonViewModel>> EditPerson(int id, PersonViewModel model, IFormFile file);

        Task<IBaseResponse<bool>> DeletePerson(int id);
    }
}
