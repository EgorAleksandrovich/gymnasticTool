﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_appl_MVVM.Data
{
    public class Rating
    {
        public Discipline Discipline { get; set; }
        public double Value { get; set; }
        public int GymnastId { get; set; }
        public int IdCompetition { get; set; }
        public int Id { get; set; }
    }
}
