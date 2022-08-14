using MVC_CongratulationApplication.DAL.Interface;
using MVC_CongratulationApplication.Domain.Entity;
using MVC_CongratulationApplication.Domain.Enum;
using MVC_CongratulationApplication.Domain.Response;
using MVC_CongratulationApplication.Service.Interface;
using IContainer = MVC_CongratulationApplication.Service.Interface.IContainer;

namespace MVC_CongratulationApplication.Service.Implementation
{
    public class Container : IContainer
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUserRepository _userRepository;

        public Container(IPersonRepository personRepository, IUserRepository userRepository)
        {
            _personRepository = personRepository;
            _userRepository = userRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Person>>> GetBirthdayPeople()
        {
            var baseResponse = new BaseResponse<IEnumerable<Person>>();
            var birthdayPeople = new List<Person>();
            try
            {
                var people = await _personRepository.GetAll();
                foreach (var person in people)
                {
                    if (person.Birthday.Day > DateTime.Now.Day && person.Birthday.Day < DateTime.Now.Day + 7 && person.Birthday.Month == DateTime.Now.Month)
                    {
                        birthdayPeople.Add(person);
                    }
                }
                if (birthdayPeople != null)
                {
                    baseResponse.Data = people;
                    baseResponse.StatusCode = StatusCode.OK;
                }
                else
                {
                    baseResponse.StatusCode = StatusCode.PeopleNotFound;
                }

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Person>>()
                {
                    Description = $"[GetBirthdayPeople] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<User>> GetUser()
        {
            var baseResponse = new BaseResponse<User>();
            try
            {
                var user = await _userRepository.GetFirst();
                if (user == null)
                {
                    baseResponse.Description = "Пользователь не найден";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = user;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetPerson] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        

    }
}
