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
using System.Windows;
using System.Windows.Controls;


namespace First_appl_MVVM.ViewModels
{
    class ViewModel : ViewModelBase
    {
        private string _selectedDiscipline;
        private List<Gymnast> _gymnasts;
        private List<Ratings> _ratings;
        private List<AllAroundRating> _allAroundRatings;
        private ObservableCollection<PersonalRatingsDiscpline> _personalRatingsDiscplins;
        private Gymnast _newGymnastInfo;
        private PersonalRatingsDiscpline _selectedPersonalRatingsDiscpline;
        private Repository _repository;
        private bool _visibilityTable;


        public ViewModel()
        {
            _repository = new Repository();
            _gymnasts = _repository.GymnastsInitialization();
            _ratings = _repository.DisciplineRatingsInitialization();
            _allAroundRatings = _repository.GetAllAroundRatings();
            _newGymnastInfo = new Gymnast();

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

            AddCommand = new RelayComand(Add);
            RemoveCommand = new RelayComand(Remove);
        }

        private void Add()
        {
            if (_newGymnastInfo.LastName == null || _newGymnastInfo.FirstName == null)
            {
                MessageBox.Show("Information about the gymnast has not been written. Please fill in!");
            }
            else
            {
                _repository.AddGymnast(_newGymnastInfo);
                NewGymnastInfo = new Gymnast();
                UpdateViewRatingDisciplines();
            }
        }

        private void Remove()
        {
            if (_selectedPersonalRatingsDiscpline != null)
            {
                _repository.RemoveGymnast(_selectedPersonalRatingsDiscpline.Id);
                UpdateViewRatingDisciplines();
            }
        }

        public void UpdateViewRatingDisciplines()
        {
            List<Gymnast> gymnasts = _repository.Gymnasts;
            ObservableCollection<PersonalRatingsDiscpline> newPersonalRatingsDiscplins = new ObservableCollection<PersonalRatingsDiscpline>();
            foreach (Gymnast gymnast in gymnasts)
            {
                PersonalRatingsDiscpline personalRatingsDiscpline = new PersonalRatingsDiscpline
                {
                    FirstName = GetFirstName(gymnasts, gymnast.ID),
                    LastName = GetLastName(gymnasts, gymnast.ID),
                    Country = GetCountry(gymnasts, gymnast.ID),
                    Rating = GetRating(gymnast.ID, _selectedDiscipline),
                    Discipline = _selectedDiscipline.ToString(),
                    Id = gymnast.ID
                };
                newPersonalRatingsDiscplins.Add(personalRatingsDiscpline);
            }
            PersonalRatingsDiscplins = newPersonalRatingsDiscplins;
        }

        public ObservableCollection<string> Disciplins { get; set; }
        public RelayComand AddCommand { get; set; }
        public RelayComand AddCommаand { get; set; }
        public RelayComand RemoveCommand { get; set; }

        public bool VisibilityTable
        {
            get
            {
                return _visibilityTable;
            }
            set
            {
                _visibilityTable = value;
                OnPropertyChanged("VisibilityTable");
            }
        }

        public Gymnast NewGymnastInfo
        {
            get
            {
                return _newGymnastInfo;
            }
            set
            {
                _newGymnastInfo = value;
                OnPropertyChanged("NewGymnastInfo");
            }
        }

        public PersonalRatingsDiscpline SelectedPersonalRatingsDiscpline
        {
            get
            {
                return _selectedPersonalRatingsDiscpline;
            }
            set
            {
                _selectedPersonalRatingsDiscpline = value;
                OnPropertyChanged("SelectedPersonalRatingsDiscpline");
            }
        }

        public ObservableCollection<PersonalRatingsDiscpline> PersonalRatingsDiscplins
        {
            get
            {
                return _personalRatingsDiscplins;
            }
            set
            {
                _personalRatingsDiscplins = value;
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
                if (_selectedDiscipline != "System.Windows.Controls.ListBoxItem: Choose a discipline")
                {
                    VisibilityTable = true;
                }
                UpdateViewRatingDisciplines();
                OnPropertyChanged("SelectedDiscipline");
            }
        }

        public string GetFirstName(List<Gymnast> gymnasts, int id)
        {
            string firstName = null;
            foreach (Gymnast gymnast in gymnasts)
            {
                if (gymnast.ID == id)
                {
                    firstName = gymnast.LastName;
                }
            }
            return firstName;
        }

        public string GetLastName(List<Gymnast> gymnasts, int id)
        {
            string name = null;
            foreach (Gymnast gymnast in gymnasts)
            {
                if (gymnast.ID == id)
                {
                    name = gymnast.FirstName;
                }
            }
            return name;
        }

        public double GetRating(int id, string inputDiscipline)
        {
            double rating = 0;
            if (inputDiscipline == "Total")
            {
                foreach (AllAroundRating allAroundRating in _allAroundRatings)
                {
                    if (allAroundRating.GymnastId == id)
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

        public string GetCountry(List<Gymnast> gymnasts, int id)
        {
            string country = null;
            foreach (Gymnast gymnast in gymnasts)
            {
                if (gymnast.ID == id)
                {
                    country = gymnast.Country;
                }
            }
            return country;
        }

        //public PersonalRatingsDiscpline FindGymnastInPerRatDis(int Id)
        //{
        //    PersonalRatingsDiscpline correspondingGymnast = null;
        //    foreach (PersonalRatingsDiscpline personalRatingsDiscpline in _personalRatingsDiscplins)
        //    {
        //        if (personalRatingsDiscpline.Id == Id)
        //        {
        //            correspondingGymnast = personalRatingsDiscpline;
        //        }
        //    }
        //    return correspondingGymnast;
        //}

        public void ChangeRating(int Id, string discipline, double newRating)
        {
            foreach (Ratings rating in _repository.DisciplineRatings)
            {
                if (rating.Discipline.ToString() == discipline)
                {
                    if (rating.GymnastId == Id)
                    {
                        rating.Rating = newRating;
                    }
                }
            }
        }
    }
}
