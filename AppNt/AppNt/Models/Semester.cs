using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppNt.Models
{
    public class Semester
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; } //Tengo que ponerlo por BD cual es. Salvo que nos enseñen a trabajar con ENUMS.

        public ICollection<Asignature> Asignatures { get; set; }

    }
}
