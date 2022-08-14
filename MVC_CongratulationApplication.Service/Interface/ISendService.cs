using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CongratulationApplication.Service.Interface
{
    public interface ISendService
    {
        Task SendEmail(string emailTo, string subject, string message);
    }
}
