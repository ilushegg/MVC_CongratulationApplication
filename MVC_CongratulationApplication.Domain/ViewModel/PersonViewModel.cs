using Microsoft.AspNetCore.Http;

namespace MVC_CongratulationApplication.Domain.ViewModel
{
    public class PersonViewModel
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public IFormFile? File { get; set; }
    }
}
