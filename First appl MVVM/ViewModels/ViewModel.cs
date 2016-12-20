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
        public ObservableCollection<Discipline> Disciplins { get; set; }

        public Discipline SelectedDiscipline 
        {
            get 
            { 
                return _selectedDiscipline; 
            }
            set
            {
                _selectedDiscipline = value;
                ChangeDiscipline(_selectedDiscipline);
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
                new Discipline { DisciplineEnum = DisciplineIs.FloorExercise},
                new Discipline { DisciplineEnum = DisciplineIs.PommelHorse},
                new Discipline { DisciplineEnum = DisciplineIs.StillRings},
                new Discipline { DisciplineEnum = DisciplineIs.Vault},
                new Discipline { DisciplineEnum = DisciplineIs.ParallelBars},
                new Discipline { DisciplineEnum = DisciplineIs.HighBar},
                new Discipline { DisciplineEnum = DisciplineIs.Total}
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
        public void ChangeDiscipline(Discipline discipline)
        {
            PersonalRatingsDiscplins = new ObservableCollection<PersonalRatingsDiscpline>
            {
                new PersonalRatingsDiscpline { firstName = GetFirstName(_gymnasts, 1), lastName = GetName(_gymnasts, 1), rating = GetRating(_ratings, 1, discipline)},
                new PersonalRatingsDiscpline { firstName = GetFirstName(_gymnasts, 2), lastName = GetName(_gymnasts, 2), rating = GetRating(_ratings, 2, discipline)},
                new PersonalRatingsDiscpline { firstName = GetFirstName(_gymnasts, 3), lastName = GetName(_gymnasts, 3), rating = GetRating(_ratings, 3, discipline)},
                new PersonalRatingsDiscpline { firstName = GetFirstName(_gymnasts, 4), lastName = GetName(_gymnasts, 4), rating = GetRating(_ratings, 4, discipline)}
            };
        }
    }
}
