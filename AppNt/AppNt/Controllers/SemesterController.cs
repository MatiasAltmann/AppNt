using AppNt.Models;
using AppNt.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RankingProfesores.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppNt.Controllers
{
    public class SemesterController : Controller
    {
        private RankingDataBaseContext _dbContext;  //Por convención los atributos privados van con un _atributo por delante y empiezan en minuscula.

        public SemesterController(RankingDataBaseContext dbContext)
        {
            _dbContext = dbContext;
        } 
        public IActionResult showSemesters()
        {
            var semesters = _dbContext.Semesters.Select(x => new SemesterViewModel
            {
                Id = x.Id,
                Type = x.Type
            }); 

            return View(semesters);
        }
    }
}
