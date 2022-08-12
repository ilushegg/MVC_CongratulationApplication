using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CongratulationApplication.Domain.ViewModel
{
    public class PersonViewModel
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}
