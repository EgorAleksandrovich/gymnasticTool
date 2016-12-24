using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace First_appl_MVVM.Data
{
    public class Repository
    {
        public List<Gymnast> Gymnasts { get; set; }
        public List<Ratings> DisciplineRatings { get; set; }

        public List<Gymnast> GetGymnasts()
        {
            Gymnasts = new List<Gymnast>
            {
                new Gymnast { ID = 1, FirstName="Egor", LastName="Bolshakov", Country="Norway" },
                new Gymnast { ID = 2, FirstName="Yuri", LastName="Dorokhov", Country="Ukraine" },
                new Gymnast { ID = 3, FirstName="Stas", LastName="Stefanovski", Country="Germani" },
                new Gymnast { ID = 4, FirstName="Roman", LastName="Palkin", Country="USA" }
            };
            return Gymnasts;
        }

        public List<Ratings> GetDisciplineRatings()
        {
            DisciplineRatings = new List<Ratings>
            {
                new Ratings { gymnastId = 1, discipline = DisciplineIs.FloorExercise, dateOfCompetition = new DateTime(2016,12,09), rating = 16.1 },
                new Ratings { gymnastId = 2, discipline = DisciplineIs.FloorExercise, dateOfCompetition = new DateTime(2016,12,09), rating = 15.7 },
                new Ratings { gymnastId = 3, discipline = DisciplineIs.FloorExercise , dateOfCompetition = new DateTime(2016,12,09), rating = 14.4 },
                new Ratings { gymnastId = 4, discipline = DisciplineIs.FloorExercise, dateOfCompetition = new DateTime(2016,12,09), rating = 15.5 },

                new Ratings { gymnastId = 1, discipline = DisciplineIs.PommelHorse, dateOfCompetition = new DateTime(2016,12,09), rating = 15.1 },
                new Ratings { gymnastId = 2, discipline = DisciplineIs.PommelHorse, dateOfCompetition = new DateTime(2016,12,09), rating = 14.7 },
                new Ratings { gymnastId = 3, discipline = DisciplineIs.PommelHorse, dateOfCompetition = new DateTime(2016,12,09), rating = 15.4 },
                new Ratings { gymnastId = 4, discipline = DisciplineIs.PommelHorse, dateOfCompetition = new DateTime(2016,12,09), rating = 14.5 },

                new Ratings { gymnastId = 1, discipline = DisciplineIs.StillRings, dateOfCompetition = new DateTime(2016,12,09), rating = 14 },
                new Ratings { gymnastId = 2, discipline = DisciplineIs.StillRings, dateOfCompetition = new DateTime(2016,12,09), rating = 13.7 },
                new Ratings { gymnastId = 3, discipline = DisciplineIs.StillRings, dateOfCompetition = new DateTime(2016,12,09), rating = 14.8 },
                new Ratings { gymnastId = 4, discipline = DisciplineIs.StillRings, dateOfCompetition = new DateTime(2016,12,09), rating = 15.2 },

                new Ratings { gymnastId = 1, discipline = DisciplineIs.Vault, dateOfCompetition = new DateTime(2016,12,09), rating = 15.1 },
                new Ratings { gymnastId = 2, discipline = DisciplineIs.Vault, dateOfCompetition = new DateTime(2016,12,09), rating = 16.7 },
                new Ratings { gymnastId = 3, discipline = DisciplineIs.Vault, dateOfCompetition = new DateTime(2016,12,09), rating = 15.4 },
                new Ratings { gymnastId = 4, discipline = DisciplineIs.Vault, dateOfCompetition = new DateTime(2016,12,09), rating = 14.5 },

                new Ratings { gymnastId = 1, discipline = DisciplineIs.ParallelBars, dateOfCompetition = new DateTime(2016,12,09), rating = 15.7 },
                new Ratings { gymnastId = 2, discipline = DisciplineIs.ParallelBars, dateOfCompetition = new DateTime(2016,12,09), rating = 14.2 },
                new Ratings { gymnastId = 3, discipline = DisciplineIs.ParallelBars, dateOfCompetition = new DateTime(2016,12,09), rating = 15.1 },
                new Ratings { gymnastId = 4, discipline = DisciplineIs.ParallelBars, dateOfCompetition = new DateTime(2016,12,09), rating = 13.9 },

                new Ratings { gymnastId = 1, discipline = DisciplineIs.HighBar, dateOfCompetition = new DateTime(2016,12,09), rating = 13.9 },
                new Ratings { gymnastId = 2, discipline = DisciplineIs.HighBar, dateOfCompetition = new DateTime(2016,12,09), rating = 14.8 },
                new Ratings { gymnastId = 3, discipline = DisciplineIs.HighBar, dateOfCompetition = new DateTime(2016,12,09), rating = 15.3 },
                new Ratings { gymnastId = 4, discipline = DisciplineIs.HighBar, dateOfCompetition = new DateTime(2016,12,09), rating = 15.0 }
            };
            return DisciplineRatings;
        }
    }
}
