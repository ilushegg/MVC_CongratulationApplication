using Microsoft.AspNetCore.Http;
using MVC_CongratulationApplication.Domain.Entity;
using MVC_CongratulationApplication.Domain.Response;
using MVC_CongratulationApplication.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CongratulationApplication.Service.Interface
{
    public interface IUserService
    {
        Task<IBaseResponse<User>> GetUser(int id);

        Task<IBaseResponse<User>> GetUser();
        Task<IBaseResponse<UserViewModel>> CreateUser(UserViewModel model);

        Task<IBaseResponse<UserViewModel>> EditUser(UserViewModel model);

        Task<IBaseResponse<bool>> DeleteActivationCode();

        Task<IBaseResponse<bool>> SendActivationCode();

    }
}
