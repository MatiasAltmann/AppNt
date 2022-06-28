using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppNt.Models
{
    public class Ranking
    {
        public ICollection<Profesor> Profesors { get; set; }
        //Podria tambien tener distintas listas ordenadas x diferentes criterios.

    }
}
