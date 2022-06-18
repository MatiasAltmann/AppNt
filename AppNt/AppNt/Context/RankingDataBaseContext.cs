using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppNt.Models;


namespace RankingProfesores.Context
{
    public class RankingDataBaseContext : DbContext
    {
        public RankingDataBaseContext(DbContextOptions<RankingDataBaseContext> options): base(options)
        {
        }

        public DbSet<Student> Estudiantes { get; set; }
        public DbSet<Profesor> Profesores{ get; set; }
        public DbSet<Asignature> Materias { get; set; }
    }
}