using AppNt.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppNt.Controllers
{
    public class UserViewModel
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

        public IEnumerable<SelectListItem> GenderVm{ get; set; }
        //Los viewModel solo pueden tener VieModel. Es por eso que hago un genero View Model.

        public int Gender { get; set; }
    }
}
