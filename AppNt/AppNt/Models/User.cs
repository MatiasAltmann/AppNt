using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;




namespace AppNt.Models
{
    public class User //Esto es valido tanto para el estudiante como para el Admin.
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int IdentificationNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int Age { get; set; }

        [EnumDataType(typeof(Gender))]

        public Gender Gender { get; set; }

        public Role Role { get; set; }
    }

        
}
