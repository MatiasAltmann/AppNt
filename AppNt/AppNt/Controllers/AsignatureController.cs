using AppNt.ViewModels;
using Microsoft.AspNetCore.Mvc;
using RankingProfesores.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppNt.Controllers
{
    public class AsignatureController : Controller
    {
        private RankingDataBaseContext _dbContext;  //Por convención los atributos privados van con un _atributo por delante y empiezan en minuscula.

        public AsignatureController(RankingDataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult showAsignatures(int idSemester)
        {
            var asignatures = _dbContext.Asignatures.Where(x => x.Id == idSemester).ToList();
            

            return View(asignatures);
        }

        
    }
}
