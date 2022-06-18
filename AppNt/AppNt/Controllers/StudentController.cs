using AppNt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RankingProfesores.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppNt.Controllers
{
    public class StudentController : Controller
    {
        private RankingDataBaseContext _dbContext;  //Por convención los atributos privados van con un _atributo por delante y empiezan en minuscula.

        public StudentController(RankingDataBaseContext dbContext)
        {
            _dbContext = dbContext;
        } // Esto lo tengo que hacer si o si para poder usarlo con la BD.

        [HttpGet]
        public IActionResult RegisterStudent(){
            var gender = _dbContext.Gender;

            List<SelectListItem> genderVmList = gender.Select(x=> new SelectListItem { 
                Text = x.Name,
                Value = x.Id.ToString()
                
            }).ToList();

            var studentVm = new StudentViewModel
            {
                GenderVm = genderVmList
            };
            return View(studentVm);
        }

        //Creo la accion de registrar usuario
        public IActionResult RegisterStudent(StudentViewModel studentVm)
        {
            var gender = _dbContext.Gender.Where(x=> x.Id == studentVm.Gender).FirstOrDefault();

            var student = new Student
            {
                Name = studentVm.Name,
                Age = studentVm.Age,
                Gender = gender,
                Email = studentVm.Email,
                IdentificationNumber = studentVm.IdentificationNumber,
                Lastname = studentVm.Lastname,
                Password = studentVm.Password,
                Username = studentVm.Username
            };

            _dbContext.Add(student); //Guardo el objeto en memoria
            _dbContext.SaveChanges(); //  Guardo en la BD
            return View();
        }
    }
}
