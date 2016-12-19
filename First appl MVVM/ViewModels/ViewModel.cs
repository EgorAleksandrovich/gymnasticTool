using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.ObjectModel;
using First_appl_MVVM.Data;

namespace First_appl_MVVM.ViewModels
{
    class ViewModel : ViewModelBase
    {
        private Discipline _selectedDiscipline;
        private List<Gymnast> _gymnasts;
        private List<Ratings> _ratings;

        public ObservableCollection<PersonalRatingsDiscpline> PersonalRatingsDiscplins { get; set; }
        public ObservableCollection<Discipline> Disciplins;

        public Discipline SelectedDiscipline 
        {
            set
            {
                _selectedDiscipline = value;
                OnPropertyChanged("SelectedDiscipline");
            }
        }

        public ViewModel()
        {
            Repository repository = new Repository();
            _gymnasts = repository.GetGymnasts();
            _ratings = repository.GetDisciplineRatings();
            
            Disciplins = new ObservableCollection<Discipline>
            {
                new Discipline { discipline = EnumDiscipline.DisciplineIs.FloorExercise},
                new Discipline { discipline = EnumDiscipline.DisciplineIs.PommelHorse},
                new Discipline { discipline = EnumDiscipline.DisciplineIs.StillRings},
                new Discipline { discipline = EnumDiscipline.DisciplineIs.Vault},
                new Discipline { discipline = EnumDiscipline.DisciplineIs.ParallelBars},
                new Discipline { discipline = EnumDiscipline.DisciplineIs.HighBar}
            };

            PersonalRatingsDiscplins = new ObservableCollection<PersonalRatingsDiscpline>
            {
                new PersonalRatingsDiscpline { firstName = GetFirstName(_gymnasts, 1), lastName = GetName(_gymnasts, 1), rating = GetRating(_ratings, 1, _selectedDiscipline)},
                new PersonalRatingsDiscpline { firstName = GetFirstName(_gymnasts, 2), lastName = GetName(_gymnasts, 2), rating = GetRating(_ratings, 2, _selectedDiscipline)},
                new PersonalRatingsDiscpline { firstName = GetFirstName(_gymnasts, 3), lastName = GetName(_gymnasts, 3), rating = GetRating(_ratings, 3, _selectedDiscipline)},
                new PersonalRatingsDiscpline { firstName = GetFirstName(_gymnasts, 4), lastName = GetName(_gymnasts, 4), rating = GetRating(_ratings, 4, _selectedDiscipline)},
            };
        }

        public string GetFirstName(List<Gymnast> gymnasts, int id)
        {
            string firstName = null;
            foreach(Gymnast gymnast in gymnasts)
            {
                if(gymnast.ID == id)
                {
                    firstName = gymnast.LastName; 
                }
            }
            return firstName;
        }

        public string GetName(List<Gymnast> gymnasts, int id)
        {
            string Name = null;
            foreach (Gymnast gymnast in gymnasts)
            {
                if (gymnast.ID == id)
                {
                    Name = gymnast.FirstName;
                }
            }
            return Name;
        }

        public double GetRating(List<Ratings> disciplineRatings, int id, Discipline discipline)
        {
            double rating = 0;
            foreach (Ratings ratings in disciplineRatings)
            {
                if (ratings.gymnastId == id)
                {
                   if (Convert.ToString(discipline) == Convert.ToString(ratings.discipline))
                   {
                       rating = ratings.rating;
                   }
                }
            }
            return rating;
        }
    }
}
