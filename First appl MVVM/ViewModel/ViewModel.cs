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
        private List<Rating> _ratings;
        private ObservableCollection<PersonalRatingsDiscpline> _personalRatingsDiscplins;
        private Gymnast _newGymnastInfo;
        private PersonalRatingsDiscpline _selectedPersonalRatingsDiscpline;
        private Repository _repository;
        private bool _visibilityTable;
        private ObservableCollection<ResultTable> _table;   // test stackpanel in menu


        public ViewModel()
        {
            _table = new ObservableCollection<ResultTable> { 
                new ResultTable {CompetitionName = "Rola"},
                new ResultTable { CompetitionName = "24 cup"}
                };
            _repository = new Repository();
            _gymnasts = new List<Gymnast>();
            _ratings = new List<Rating>();
            _newGymnastInfo = new Gymnast();
            UpdateViewRatings();

            Disciplins = new ObservableCollection<string>
            {
                "Choose a discipline",
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
            SaveRatingsCommand = new RelayComand(SaveRatings);
            UpdateViewRatingsCommand = new RelayComand(UpdateViewRatings);
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
                NewGymnastInfo = new Gymnast();         // обнуление текст бокса
                UpdateViewRatings();
            }
        }

        private void Remove()
        {
            if (_selectedPersonalRatingsDiscpline != null)
            {
                _repository.RemoveGymnast(_selectedPersonalRatingsDiscpline.Id);
                _repository.RemoveDisciplineRatings(_selectedPersonalRatingsDiscpline.Id);
                UpdateViewRatings();
            }
        }

        public void SaveRatings()
        {
            foreach (PersonalRatingsDiscpline personalRatingsDiscpline in _personalRatingsDiscplins)
            {
                if (personalRatingsDiscpline.IsUpdated == true)
                {
                    _repository.SaveRatings(personalRatingsDiscpline);
                }
            }
        }

        public void UpdateViewRatings()
        {
            if(_personalRatingsDiscplins != null)
            {
                SaveRatings();
            }
            _gymnasts = _repository.GetGymnasts();
            _ratings = _repository.GetDisciplineRatings();
            ObservableCollection<PersonalRatingsDiscpline> newPersonalRatingsDiscplins = new ObservableCollection<PersonalRatingsDiscpline>();
            foreach (Gymnast gymnast in _gymnasts)
            {
                PersonalRatingsDiscpline personalRatingsDiscpline = new PersonalRatingsDiscpline
                {
                    FirstName = GetFirstName(_gymnasts, gymnast.ID),
                    LastName = GetLastName(_gymnasts, gymnast.ID),
                    Country = GetCountry(_gymnasts, gymnast.ID),
                    Rating = GetRating(gymnast.ID, _selectedDiscipline),
                    IsUpdated = false,
                    Discipline = _selectedDiscipline,
                    Id = gymnast.ID,
                    IdRating = GetIdRating(gymnast.ID, _selectedDiscipline, _ratings)
                };
                newPersonalRatingsDiscplins.Add(personalRatingsDiscpline);
            }
            PersonalRatingsDiscplins = newPersonalRatingsDiscplins;
        }

        public ObservableCollection<string> Disciplins { get; set; }
        public RelayComand AddCommand { get; set; }
        public RelayComand AddCommаand { get; set; }
        public RelayComand RemoveCommand { get; set; }
        public RelayComand SaveRatingsCommand { get; set; }
        public RelayComand UpdateViewRatingsCommand { get; set; }

        public ObservableCollection<ResultTable> Table
        {
            get
            {
                return _table;
            }
            set
            {
                _table = value;
                OnPropertyChanged("Table");
            }
        }

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
                if (_selectedDiscipline != "Choose a discipline")
                {
                    VisibilityTable = true;
                }
                else
                {
                    VisibilityTable = false;
                }
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
                    firstName = gymnast.FirstName;
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
                    name = gymnast.LastName;
                }
            }
            return name;
        }

        public double GetRating(int id, string inputDiscipline)
        {
            double rating = 0;
            if (inputDiscipline == "Total")
            {
                rating = GetAllAroundRatingGymnast(id, _ratings);
            }
            else
            {
                foreach (Rating ratings in _ratings)
                {
                    if (ratings.GymnastId == id)
                    {
                        if (inputDiscipline == ratings.Discipline.ToString())
                        {
                            rating = ratings.Value;
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

        public int GetIdRating(int gymnstId, string inputDiscipline, List<Rating> ratings)
        {
            int idRating = 0;
            if(inputDiscipline != null && inputDiscipline != "Total" && inputDiscipline != "Choose a discipline")
            {
                DisciplineIs discpline = (DisciplineIs)Enum.Parse(typeof(DisciplineIs), inputDiscipline);
                Rating rating = ratings.Find(r => r.GymnastId == gymnstId && r.Discipline == discpline);
                if(rating != null)
                {
                    idRating = rating.Id;
                }
                else
                {
                    idRating = 0;
                }
            }
            return idRating;
        }

        public double GetAllAroundRatingGymnast(int gymnastId, List<Rating> disciplineRatings)
        {
            double allAroundRating = 0;
            foreach (Rating rating in disciplineRatings)
            {
                if (rating.GymnastId == gymnastId)
                {
                    allAroundRating += rating.Value;
                }
            }
            return allAroundRating;
        }
    }
}