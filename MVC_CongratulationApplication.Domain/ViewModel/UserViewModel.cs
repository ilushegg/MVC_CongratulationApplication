using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CongratulationApplication.Domain.ViewModel
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime SendingTime { get; set; }
        public string isAllowSending { get; set; }
    }
}
