using AppNt.Models;
using Microsoft.AspNetCore.Mvc;
using RankingProfesores.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppNt.Controllers
{
    public class SetupController : Controller
    {

        private RankingDataBaseContext _dbContext;  //Por convención los atributos privados van con un _atributo por delante y empiezan en minuscula.

        // Esto lo tengo que hacer si o si para poder usarlo con la BD.
        public SetupController(RankingDataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Setup()
        {
            var gender = new List<Gender> {
                new Gender {
                    Name = "Male"
                },

                new Gender {
                    Name = "Feminine"
                }
            };

            _dbContext.AddRange(gender);
            _dbContext.SaveChanges();
                
            return View("ok");
        }
    }
}
