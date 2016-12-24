using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_appl_MVVM.Data
{
    public class Ratings
    {
        public DisciplineIs discipline { get; set; }
        public double rating { get; set; }
        public int gymnastId { get; set; }
        public DateTime dateOfCompetition { get; set; }
    }
}
