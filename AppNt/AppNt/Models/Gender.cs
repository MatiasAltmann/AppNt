
using System.ComponentModel.DataAnnotations;

namespace AppNt.Models
{
    public class Gender
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
