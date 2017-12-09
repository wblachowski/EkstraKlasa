using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ekstraklasa
{
    class MatchViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;
        public event delegateUpdateControl UpdateContentEvent = null;

        public MatchViewModel()
        {
            Score = "Brak danych";
        }

        public MatchViewModel(MatchEntity match)
        {
            Match = match;
            Score = String.Format("{0}:{1}", match.ScoreHost, match.ScoreGuest);
            UpdateGoals();
        }

        private ICommand _OpenDialog;
        public ICommand OpenDialog
        {
            get
            {
                if (_OpenDialog == null)
                {
                    _OpenDialog = new RelayCommand(param => ExecuteRunDialog(param));
                }
                return _OpenDialog;
            }
        }

        private MatchEntity _Match;
        public MatchEntity Match
        {
            get
            {
                return _Match;
            }
            set
            {
                if(_Match != value)
                {
                    _Match = value;
                    OnPropertyChanged("Match");
                }
            }
        }

        private string _Score;
        public string Score
        {
            get
            {
                return _Score;
            }
            set
            {
                if (_Score != value)
                {
                    _Score = value;
                    OnPropertyChanged("Score");
                }
            }
        }

        private ObservableCollection<GoalControl> _GoalsA = new ObservableCollection<GoalControl>();
        public ObservableCollection<GoalControl> GoalsA
        {
            get
            {
                return _GoalsA;
            }
            set
            {
                if (_GoalsA != value)
                {
                    _GoalsA = value;
                    OnPropertyChanged("GoalsA");
                }
            }
        }

        private ObservableCollection<GoalControl> _GoalsB = new ObservableCollection<GoalControl>();
        public ObservableCollection<GoalControl> GoalsB
        {
            get
            {
                return _GoalsB;
            }
            set
            {
                if (_GoalsB != value)
                {
                    _GoalsB = value;
                    OnPropertyChanged("GoalsB");
                }
            }
        }

        private async void UpdateGoals()
        {
            List<GoalEntity> goals = await GetGoalsByIDAsync();
            ObservableCollection<GoalControl> tempA = new ObservableCollection<GoalControl>();
            ObservableCollection<GoalControl> tempB = new ObservableCollection<GoalControl>();
            foreach (GoalEntity goal in goals)
            {
                if (goal.HostGoal)
                {
                    tempA.Add(new GoalControl(goal, true));
                }
                else
                {
                    tempB.Add(new GoalControl(goal, false));
                }
            }
            GoalsA = tempA;
            GoalsB = tempB;
        }

        private async Task<List<GoalEntity>> GetGoalsByIDAsync()
        {
            return await Task.Run(() =>
            {
                return MainModel.GetGoalsByID(Match.ID);
            });

        }

        private async void ExecuteRunDialog(object o)
        {
            var view = new DeleteMatchDialog(Match.Host + " " + Score + " " + Match.Guest);
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
            if(result!= null && (bool)result == true)
            {
                DeleteMatch();
            }
        }

        private async void DeleteMatch()
        {
            await Task.WhenAll( Task.Run(()=>MainModel.DeleteGoal(Match.ID)),  Task.Run(() => MainModel.DeleteMatch(Match.ID)));
            if (UpdateContentEvent != null)
            {
                UpdateContentEvent(0);
                UpdateContentEvent(1);
                UpdateContentEvent(4);
            }
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here.");
        }


        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
