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
        private AllRating _selectedDiscipline;

        public ObservableCollection<AllRating> GymnasticsRatingsDiscipline { get; set; }
        public ObservableCollection<Gymnast> Gymnasts { get; set; }
        public ObservableCollection<DisciplineRating> DisciplineRatings { get; set; }

        public AllRating GymnastsRatings
        {
            get { return _selectedDiscipline; }
            set
            {
                _selectedDiscipline = value;
                OnPropertyChanged("GymnasticsRatings");
            }
        }

        public ViewModel()
        {
            AllRating yuriRatings = new AllRating() { FloorExercise = 13, PommelHorse = 14, StillRings = 15, Vault = 13.4, ParallelBars = 15.4, HighBar = 15.6, Total = 75 };
            AllRating egorRatings = new AllRating() { FloorExercise = 13.5, PommelHorse = 14.5, StillRings = 15.3, Vault = 12.4, ParallelBars = 14.4, HighBar = 14.6, Total = 76 };
            AllRating stasRatings = new AllRating() { FloorExercise = 15, PommelHorse = 15.4, StillRings = 14, Vault = 15, ParallelBars = 13, HighBar = 13, Total = 78 };
            AllRating romanRatings = new AllRating() { FloorExercise = 14.2, PommelHorse = 14.6, StillRings = 13, Vault = 16, ParallelBars = 14.5, HighBar = 15, Total = 8 };

            Gymnasts = new ObservableCollection<Gymnast>
            {
                new Gymnast { FirstName="Dorokhov", LastName="Yra", Country="Ukraina", Ratings = yuriRatings},
                new Gymnast { FirstName="Bolshakov", LastName="Egor", Country="Norway", Ratings = egorRatings},
                new Gymnast { FirstName="Stefanovskii", LastName="Stas", Country="Germani", Ratings = stasRatings},
                new Gymnast { FirstName="Palkin", LastName="Roman", Country="USA", Ratings = romanRatings}

            };

            DisciplineRatings = new ObservableCollection<DisciplineRating>
            {
                new DisciplineRating { rating=}
            };
        }
    }
}
