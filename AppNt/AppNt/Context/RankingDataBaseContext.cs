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

        public DbSet<Student> Students { get; set; }
        public DbSet<Profesor> Profesors{ get; set; }
        public DbSet<Asignature> Asignatures { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Semester> Semesters { get; set; }
    }
}