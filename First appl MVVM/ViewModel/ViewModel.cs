using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.ObjectModel;
using First_appl_MVVM.Data;
using First_appl_MVVM.Command;

namespace First_appl_MVVM.ViewModels
{
    class ViewModel : ViewModelBase
    {
        private string _selectedDiscipline;
        private List<Gymnast> _gymnasts;
        private List<Ratings> _ratings;
        private List<AllAroundRating> _allAroundRatings;
        public ObservableCollection<PersonalRatingsDiscpline> NewPersonalRatingsDiscplins { get; set; }
        public ObservableCollection<PersonalRatingsDiscpline> personalRatingsDiscplins;
        public ObservableCollection<string> Disciplins { get; set; }
        public Gymnast newGymnastInfo;   // !!!!!!!
        

        public RelayComand AddCommand { get; set; }

        private void Add()
        {
            PersonalRatingsDiscpline newGymnast = new PersonalRatingsDiscpline();   // !!!!!!!
            int indexNewGymnast = personalRatingsDiscplins.Count();                     // !!!!!!!
            PersonalRatingsDiscplins.Insert(indexNewGymnast, newGymnast);           // !!!!!!!
            newGymnast.FirstName = newGymnastInfo.FirstName;                         // !!!!!!!
            newGymnast.LastName = newGymnastInfo.LastName;                          // !!!!!!!
            newGymnast.Id = indexNewGymnast;                                        // !!!!!!!
            OnPropertyChanged("PersonalRatingsDiscplins");                          // !!!!!!!
        }

        public Gymnast NewGymnastInfo // !!!!!!!
        {
            get
            {
                return newGymnastInfo;
            }
            set
            {
                newGymnastInfo = value;
                OnPropertyChanged("NewGymnast");
            }
        }

        public ObservableCollection<PersonalRatingsDiscpline> PersonalRatingsDiscplins
        {
            get
            {
                return personalRatingsDiscplins;
            }
            set
            {
                personalRatingsDiscplins = value;
                OnPropertyChanged("PersonalRatingsDiscplins");
            }
        }
        
        public string SelectedDiscipline 
        {
            get 
            { 
                return _selectedDiscipline;
            }
            set
            {
                _selectedDiscipline = value;
                ChangeDiscipline();
                OnPropertyChanged("SelectedDiscipline");
            }
        }

        public ViewModel()
        {
            Repository repository = new Repository();
            _gymnasts = repository.GetGymnasts();
            _ratings = repository.GetDisciplineRatings();
            _allAroundRatings = repository.GetAllAroundRatings();
            newGymnastInfo = new Gymnast();   // !!

            Disciplins = new ObservableCollection<string>
            {
                DisciplineIs.FloorExercise.ToString(),
                DisciplineIs.PommelHorse.ToString(),
                DisciplineIs.StillRings.ToString(),
                DisciplineIs.Vault.ToString(),
                DisciplineIs.ParallelBars.ToString(),
                DisciplineIs.HighBar.ToString(),
                "Total"
            };

            PersonalRatingsDiscplins = new ObservableCollection<PersonalRatingsDiscpline>
            {
                new PersonalRatingsDiscpline { FirstName = GetFirstName(_gymnasts, 1), LastName = GetName(_gymnasts, 1), Rating = 0, Id = 1, Discipline = "ratings"},
                new PersonalRatingsDiscpline { FirstName = GetFirstName(_gymnasts, 2), LastName = GetName(_gymnasts, 2), Rating = 0, Id = 2, Discipline = "ratings"},
                new PersonalRatingsDiscpline { FirstName = GetFirstName(_gymnasts, 3), LastName = GetName(_gymnasts, 3), Rating = 0, Id = 3, Discipline = "ratings"},
                new PersonalRatingsDiscpline { FirstName = GetFirstName(_gymnasts, 4), LastName = GetName(_gymnasts, 4), Rating = 0, Id = 4, Discipline = "ratings"}
            };

            AddCommand = new RelayComand(Add);
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

        public double GetRating( int id, string inputDiscipline)
        {
            double rating = 0;
            if (inputDiscipline == "Total")
            {
                foreach(AllAroundRating allAroundRating in _allAroundRatings)
                {
                    if(allAroundRating.GymnastId == id)
                    {
                        rating = allAroundRating.AllAroundRatig;
                    }
                }
            }
            else
            {
                foreach (Ratings ratings in _ratings)
                {
                    if (ratings.GymnastId == id)
                    {
                        if (inputDiscipline == ratings.Discipline.ToString())
                        {
                            rating = ratings.Rating;
                        }
                    }
                }
            }
            return rating;
        }
        public void ChangeDiscipline()
        {
            NewPersonalRatingsDiscplins  = new ObservableCollection<PersonalRatingsDiscpline>
            {
                new PersonalRatingsDiscpline { FirstName = GetFirstName(_gymnasts, 1), LastName = GetName(_gymnasts, 1), Rating = GetRating(1,_selectedDiscipline), Id = 1, Discipline = _selectedDiscipline.ToString()},
                new PersonalRatingsDiscpline { FirstName = GetFirstName(_gymnasts, 2), LastName = GetName(_gymnasts, 2), Rating = GetRating(2,_selectedDiscipline), Id = 2, Discipline = _selectedDiscipline.ToString()},
                new PersonalRatingsDiscpline { FirstName = GetFirstName(_gymnasts, 3), LastName = GetName(_gymnasts, 3), Rating = GetRating(3,_selectedDiscipline), Id = 3, Discipline = _selectedDiscipline.ToString()},
                new PersonalRatingsDiscpline { FirstName = GetFirstName(_gymnasts, 4), LastName = GetName(_gymnasts, 4), Rating = GetRating(4,_selectedDiscipline), Id = 4, Discipline = _selectedDiscipline.ToString()}
            };
            PersonalRatingsDiscplins = NewPersonalRatingsDiscplins;
        }
    }
}
