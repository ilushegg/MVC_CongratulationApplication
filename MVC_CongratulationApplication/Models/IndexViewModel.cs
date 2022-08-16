using MVC_CongratulationApplication.Domain.Entity;

namespace MVC_CongratulationApplication.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Person> People { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
