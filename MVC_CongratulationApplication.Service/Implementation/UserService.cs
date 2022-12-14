using Microsoft.Extensions.Configuration;
using MVC_CongratulationApplication.DAL.Interface;
using MVC_CongratulationApplication.Domain.Entity;
using MVC_CongratulationApplication.Domain.Enum;
using MVC_CongratulationApplication.Domain.Response;
using MVC_CongratulationApplication.Domain.ViewModel;
using MVC_CongratulationApplication.Service.Interface;

namespace MVC_CongratulationApplication.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITimedHostedService _timedHostedService;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, ITimedHostedService timedHostedService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _timedHostedService = timedHostedService;
            _configuration = configuration;
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

        public async Task<IBaseResponse<UserViewModel>> CreateUser(UserViewModel model)
        {
            var baseResponse = new BaseResponse<UserViewModel>();
            try
            {
                bool allow = true;
                if (model.isAllowSending.Equals("false"))
                {
                    allow = false;
                }
                var user = new User()
                {
                    Name = model.Name,
                    Email = model.Email,
                    SendingTime = model.SendingTime,
                    isAllowSending = allow
                };
                user.ActivationCode = Guid.NewGuid().ToString().Substring(0, 10);
                await _userRepository.Create(user);
                SendConfirm(user, _configuration["Host:Localhost"] + "/users/activate/" + user.ActivationCode);
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserViewModel>()
                {
                    Description = $"[CreateUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }

        }

        public async Task<IBaseResponse<UserViewModel>> EditUser(UserViewModel model)
        {
            var response = await GetUser();
            if (response.StatusCode == StatusCode.UserNotFound)
            {
                return await CreateUser(model);
            }
            else
            {
                var baseResponse = new BaseResponse<UserViewModel>();
                try
                {
                    bool allow = true;
                    if (model.isAllowSending.Equals("false"))
                    {
                        allow = false;
                    }
                    var user = await _userRepository.GetFirst();

                    if (model.Email != user.Email)
                    {
                        user.ActivationCode = Guid.NewGuid().ToString().Substring(0, 10);
                    }
                    user.Name = model.Name;
                    user.Email = model.Email;
                    user.SendingTime = model.SendingTime;
                    user.isAllowSending = allow;
                    await _userRepository.Edit(user);
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                catch (Exception ex)
                {
                    return new BaseResponse<UserViewModel>()
                    {
                        Description = $"[EditUser] : {ex.Message}",
                        StatusCode = StatusCode.InternalServerError
                    };
                }
            }
        }

        public async Task<IBaseResponse<bool>> DeleteActivationCode()
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var user = await _userRepository.GetFirst();
                if (user != null)
                {
                    user.ActivationCode = null;
                    await _userRepository.Edit(user);
                    baseResponse.StatusCode = StatusCode.OK;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteActivationCode] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> SendActivationCode()
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var user = await _userRepository.GetFirst();

                user.ActivationCode = Guid.NewGuid().ToString().Substring(0, 10);
                await _userRepository.Edit(user);
                baseResponse.StatusCode = StatusCode.OK;
                SendConfirm(user, _configuration["Host:Localhost"] + "/users/activate/" + user.ActivationCode);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[SendActivationCode] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        private void SendConfirm(User user, string link)
        {
            String message = String.Format(
                "Привет, {0}!\n" +
                        "Добро пожаловать в приложение, где Вы никогда не забудете о днях рождениях своих знакомых, друзей, товарищей!\n" +
                        "Пожалуйста, подтвердите свой электронный адрес перейдя по ссылке: {1}",
                user.Name, link
            );
            _timedHostedService.Initialize(user.Email, message, "Активация");
            try
            {
                _timedHostedService.SendEmail();
            }
            catch (Exception ex)
            {

            }
            _timedHostedService.Dispose();

        }

    }
}
