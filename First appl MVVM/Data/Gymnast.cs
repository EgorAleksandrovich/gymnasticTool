using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace First_appl_MVVM.Data
{
    public class Gymnast
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }

        public AllRating Ratings { get; set; }

        public double CalcTotal(double FloorExercise, double PommeHorse,
            double StillRings, double Vault, double ParallelBars,
            double HighBar)
        {
            double result;
            result = FloorExercise + PommeHorse + StillRings + Vault
                + ParallelBars + HighBar;
            return result;
        }
    }
}
