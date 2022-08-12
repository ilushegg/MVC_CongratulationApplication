﻿using MVC_CongratulationApplication.DAL.Interface;
using MVC_CongratulationApplication.Domain.Entity;
using MVC_CongratulationApplication.Domain.Enum;
using MVC_CongratulationApplication.Domain.Response;
using MVC_CongratulationApplication.Domain.ViewModel;
using MVC_CongratulationApplication.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace MVC_CongratulationApplication.Service.Implementation
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PersonService(IPersonRepository personRepository, IWebHostEnvironment webHostEnvironment)
        {
            _personRepository = personRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IBaseResponse<IEnumerable<Person>>> GetPeople()
        {
            var baseResponse = new BaseResponse<IEnumerable<Person>>();
            try
            {
                var people = await _personRepository.GetAll();
                if(people.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                baseResponse.Data = people;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<IEnumerable<Person>>()
                {
                    Description = $"[GetPeople] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Person>> GetPerson(int id)
        {
            var baseResponse = new BaseResponse<Person>();
            try
            {
                var person = await _personRepository.Get(id);
                if(person == null)
                {
                    baseResponse.Description = "Пользователь не найден";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = person;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<Person>()
                {
                    Description = $"[GetPerson] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Person>> GetPersonByName(string name)
        {
            var baseResponse = new BaseResponse<Person>();
            try
            {
                var person = await _personRepository.GetByName(name);
                if (person == null)
                {
                    baseResponse.Description = "Пользователь не найден";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = person;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Person>()
                {
                    Description = $"[GetPersonByName] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<PersonViewModel>> CreatePerson(PersonViewModel model, IFormFile file, string webPath)
        {
            var baseResponse = new BaseResponse<PersonViewModel>();
            Console.WriteLine(webPath);
            try
            {
                var person = new Person()
                {
                    Name = model.Name,
                    Birthday = model.Birthday
                };
                if (file != null)
                {
                    var uniqueFileName = GetUniqueFileName(file.FileName);
                    var uploads = Path.Combine(webPath, "uploads");
                    var filePath = Path.Combine(uploads, uniqueFileName);

                    file.CopyTo(new FileStream(filePath, FileMode.Create));
                    person.Filename = filePath;
                }

                await _personRepository.Create(person);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
                
                
            }
            catch (Exception ex)
            {
                return new BaseResponse<PersonViewModel>()
                {
                    Description = $"[CreatePerson] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<PersonViewModel>> EditPerson(int id, PersonViewModel model, IFormFile file)
        {
            var baseResponse = new BaseResponse<PersonViewModel>();
            try
            {
                var person = await _personRepository.Get(id);
                if (file != null)
                {
                    var uniqueFileName = GetUniqueFileName(file.FileName);
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    var filePath = Path.Combine(uploads, uniqueFileName);

                    file.CopyTo(new FileStream(filePath, FileMode.Create));
                    person.Filename = filePath;
                }
                person.Name = model.Name;
                person.Birthday = model.Birthday;

                await _personRepository.Edit(person);
                return baseResponse;


            }
            catch (Exception ex)
            {
                return new BaseResponse<PersonViewModel>()
                {
                    Description = $"[EditPerson] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeletePerson(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var person = await _personRepository.Get(id);
                if (person == null)
                {
                    baseResponse.Description = "Пользователь не найден";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                await _personRepository.Delete(person);
                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeletePerson] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        
    }
}