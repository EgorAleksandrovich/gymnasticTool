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
        public List<AllAroundRating> AllAroundRatings { get; set; }

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
                new Ratings { GymnastId = 1, Discipline = DisciplineIs.FloorExercise, DateOfCompetition = new DateTime(2016,12,09), Rating = 16.1 },
                new Ratings { GymnastId = 2, Discipline = DisciplineIs.FloorExercise, DateOfCompetition = new DateTime(2016,12,09), Rating = 15.7 },
                new Ratings { GymnastId = 3, Discipline = DisciplineIs.FloorExercise , DateOfCompetition = new DateTime(2016,12,09), Rating = 14.4 },
                new Ratings { GymnastId = 4, Discipline = DisciplineIs.FloorExercise, DateOfCompetition = new DateTime(2016,12,09), Rating = 15.5 },

                new Ratings { GymnastId = 1, Discipline = DisciplineIs.PommelHorse, DateOfCompetition = new DateTime(2016,12,09), Rating = 15.1 },
                new Ratings { GymnastId = 2, Discipline = DisciplineIs.PommelHorse, DateOfCompetition = new DateTime(2016,12,09), Rating = 14.7 },
                new Ratings { GymnastId = 3, Discipline = DisciplineIs.PommelHorse, DateOfCompetition = new DateTime(2016,12,09), Rating = 15.4 },
                new Ratings { GymnastId = 4, Discipline = DisciplineIs.PommelHorse, DateOfCompetition = new DateTime(2016,12,09), Rating = 14.5 },

                new Ratings { GymnastId = 1, Discipline = DisciplineIs.StillRings, DateOfCompetition = new DateTime(2016,12,09), Rating = 14 },
                new Ratings { GymnastId = 2, Discipline = DisciplineIs.StillRings, DateOfCompetition = new DateTime(2016,12,09), Rating = 13.7 },
                new Ratings { GymnastId = 3, Discipline = DisciplineIs.StillRings, DateOfCompetition = new DateTime(2016,12,09), Rating = 14.8 },
                new Ratings { GymnastId = 4, Discipline = DisciplineIs.StillRings, DateOfCompetition = new DateTime(2016,12,09), Rating = 15.2 },

                new Ratings { GymnastId = 1, Discipline = DisciplineIs.Vault, DateOfCompetition = new DateTime(2016,12,09), Rating = 15.1 },
                new Ratings { GymnastId = 2, Discipline = DisciplineIs.Vault, DateOfCompetition = new DateTime(2016,12,09), Rating = 16.7 },
                new Ratings { GymnastId = 3, Discipline = DisciplineIs.Vault, DateOfCompetition = new DateTime(2016,12,09), Rating = 15.4 },
                new Ratings { GymnastId = 4, Discipline = DisciplineIs.Vault, DateOfCompetition = new DateTime(2016,12,09), Rating = 14.5 },

                new Ratings { GymnastId = 1, Discipline = DisciplineIs.ParallelBars, DateOfCompetition = new DateTime(2016,12,09), Rating = 15.7 },
                new Ratings { GymnastId = 2, Discipline = DisciplineIs.ParallelBars, DateOfCompetition = new DateTime(2016,12,09), Rating = 14.2 },
                new Ratings { GymnastId = 3, Discipline = DisciplineIs.ParallelBars, DateOfCompetition = new DateTime(2016,12,09), Rating = 15.1 },
                new Ratings { GymnastId = 4, Discipline = DisciplineIs.ParallelBars, DateOfCompetition = new DateTime(2016,12,09), Rating = 13.9 },

                new Ratings { GymnastId = 1, Discipline = DisciplineIs.HighBar, DateOfCompetition = new DateTime(2016,12,09), Rating = 13.9 },
                new Ratings { GymnastId = 2, Discipline = DisciplineIs.HighBar, DateOfCompetition = new DateTime(2016,12,09), Rating = 14.8 },
                new Ratings { GymnastId = 3, Discipline = DisciplineIs.HighBar, DateOfCompetition = new DateTime(2016,12,09), Rating = 15.3 },
                new Ratings { GymnastId = 4, Discipline = DisciplineIs.HighBar, DateOfCompetition = new DateTime(2016,12,09), Rating = 15.0 }
            };
            return DisciplineRatings;
        }
        public List<AllAroundRating> GetAllAroundRatings()
        {
            AllAroundRatings = new List<AllAroundRating>
            {
                new AllAroundRating { DateOfCompetition = new DateTime(2016,12,09), GymnastId = 1, AllAroundRatig = GetAllAroundRatingGymnast(1, DisciplineRatings)},
                new AllAroundRating { DateOfCompetition = new DateTime(2016,12,09), GymnastId = 2, AllAroundRatig = GetAllAroundRatingGymnast(2, DisciplineRatings)},
                new AllAroundRating { DateOfCompetition = new DateTime(2016,12,09), GymnastId = 3, AllAroundRatig = GetAllAroundRatingGymnast(3, DisciplineRatings)},
                new AllAroundRating { DateOfCompetition = new DateTime(2016,12,09), GymnastId = 4, AllAroundRatig = GetAllAroundRatingGymnast(4, DisciplineRatings)}
            };
            return AllAroundRatings;
        }
         public double GetAllAroundRatingGymnast(int gymnastId, List<Ratings> disciplineRatings)
        {
             double allAroundRating = 0;
             foreach(Ratings rating in disciplineRatings)
             {
                 if (rating.GymnastId == gymnastId)
                 {
                     allAroundRating += rating.Rating;
                 }
             }
             return allAroundRating;
        }
    }
}
