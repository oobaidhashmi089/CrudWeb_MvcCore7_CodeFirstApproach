﻿using System.ComponentModel.DataAnnotations;

namespace CrudWebE.Models
{
    public class UpdateEmployee
    {
        [Key]
        public int EmpId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateofBirth { get; set; }
        public double Salary { get; set; }
        public string Department { get; set; }

    }
}
