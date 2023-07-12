using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CommonLayer.Model
{
    public class EmpRegModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="{0} cannot be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} cannot be empty")]
        public string ProfileImage { get; set; }
        [Required(ErrorMessage = "{0} cannot be empty")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "{0} cannot be empty")]
        public string Department { get; set; }
        [Required(ErrorMessage = "{0} cannot be empty")]
        public int Salary { get; set; }
        [Required(ErrorMessage = "{0} cannot be empty")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "{0} cannot be empty")]
        public string Notes { get; set; }

    }
}
