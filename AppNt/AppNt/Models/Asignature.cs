using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppNt.Models
{
    public class Asignature
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int SemesterId { get; set; }
        public Semester Semester { get; set; }

        public ICollection<Profesor> Profesors { get; set; } 
    }
}