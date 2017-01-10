using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_appl_MVVM.Data
{
    public class Ratings
    {
        public DisciplineIs Discipline { get; set; }
        public double Rating { get; set; }
        public int GymnastId { get; set; }
        public DateTime DateOfCompetition { get; set; }
    }
}
