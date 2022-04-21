using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Models
{
    public class ApplicationIdentityUser : IdentityUser
    {
        public string City { get; set; }
    }
}
