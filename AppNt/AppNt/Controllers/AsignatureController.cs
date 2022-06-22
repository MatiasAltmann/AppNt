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

        public IActionResult ShowAsignatures(int idSemester)
        {
            var asignatures = new List<AsignatureViewModel>();

            foreach (var item in _dbContext.Asignatures)
            {
                bool a = item.Semester.Id == 2;
                if(item.Id == idSemester) //Me falta saber como comparat el semesterId -> q me genera la base de datos
                {
                    asignatures.Add(new AsignatureViewModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Semester = item.Semester
                    });
                }
                                 
            }


            return View(asignatures);
        }

        
    }
}
