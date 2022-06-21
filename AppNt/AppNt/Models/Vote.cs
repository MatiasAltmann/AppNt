﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppNt.Models
{
    public class Vote
    {
        public int Id { get; set; } //AutoIncremental x defecto es PK.

        public User User { get; set; }

        public Profesor Profesor { get; set; }

        public bool valueVote { get; set; } //Si es true -> 1 , si es False es 0 -> Si no existe bajo el usuario no VOTO.
    }
}
