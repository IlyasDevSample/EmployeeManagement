using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        private int id;
        private string name;
        private string email;
        private string departement;
        private string photoPath;
        
        public int Id { get => id; set => id = value; }
        [Required]
        [MaxLength(50,ErrorMessage ="Name cannot Exeed 50 characters")]
        public string Name { get => name; set => name = value; }
        [Required]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$",ErrorMessage ="Invalid Email Format")]
        [Display(Name ="Office Email")]
        public string Email { get => email; set => email = value; }
        [Required]
        public string Departement { get => departement; set => departement = value; }
        public string PhotoPath { get => photoPath; set => photoPath = value; }
    }
}
