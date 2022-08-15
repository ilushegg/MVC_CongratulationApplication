using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CongratulationApplication.Service.Interface
{
    public interface ISendService
    {
        Task Initialize(string Email, string Subject, string Message, DateTime Time);
        Task Initialize(string Email, string Subject, string Message);
        Task Initialize(string Email, DateTime Time);
        Task Initialize(string Message);
        Task SendEmail();
        void CheckingAndSending(object obj);



    }
}
