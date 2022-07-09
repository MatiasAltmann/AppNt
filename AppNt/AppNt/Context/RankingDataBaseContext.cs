using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppNt.Models;
using AppNt.Controllers;




namespace RankingProfesores.Context
{
    public class RankingDataBaseContext : DbContext
    {
        public RankingDataBaseContext(DbContextOptions<RankingDataBaseContext> options): base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Profesor> Profesors{ get; set; }
        public DbSet<Asignature> Asignatures { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<AppNt.Models.VotoProfesor> VotoProfesor { get; set; }






    }
}