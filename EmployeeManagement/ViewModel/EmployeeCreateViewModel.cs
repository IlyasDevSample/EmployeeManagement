﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModel
{
    public class EmployeeCreateViewModel
    {

        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid Email Format")]
        [Display(Name = "Office Email")]
        public string Email { get; set; }
        [Required]
        public string Departement { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
