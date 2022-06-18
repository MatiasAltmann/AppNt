using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppNt.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int IdentificationNumber { get; set; }

        [Required]
        public string Username { get; set; }

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
        }
    }
