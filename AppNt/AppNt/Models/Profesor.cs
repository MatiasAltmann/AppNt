using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppNt.Models
{
    public class Profesor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Age { get; set; }

        public string Photo { get; set; }

        public int AsignatureId { get; set; }

        public Asignature Asignature { get; set; }

        public ICollection<Vote> Vote { get; set; }



    }
}
