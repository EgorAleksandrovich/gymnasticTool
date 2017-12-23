using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_appl_MVVM.Data
{
    public class PersonalRatingsDiscpline
    {
        private double _rating;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Discipline { get; set; }
        public int Id { get; set; }
        public int IdRating { get; set; }
        public int CompetitionId { get; set; }
        public double Rating
        {
            get 
            {
                return _rating;
            }
            set
            {
                _rating = value;
                IsUpdated = true;
            }
        }
        public bool IsUpdated { get; set; }
    }
}