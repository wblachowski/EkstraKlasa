using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ekstraklasa
{
    public delegate void delegateChangeControl(UserControl control);

    class TeamsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;
        public event delegateChangeControl ChangeContentEvent = null;

        public TeamsViewModel()
        {
            UpdateTeams();
        }

        private ICommand _DeleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if(_DeleteCommand == null)
                {
                    _DeleteCommand = new RelayCommand(DeleteTeam);
                }
                return _DeleteCommand;
            }
        }

        private ObservableCollection<TeamEntity> _Teams = new ObservableCollection<TeamEntity>();
        public ObservableCollection<TeamEntity> Teams
        {
            get
            {
                return _Teams;
            }
            set
            {
                if (_Teams != value)
                {
                    _Teams = value;
                    OnPropertyChanged("Teams");
                }
            }
        }

        private TeamEntity _SelectedItem;
        public TeamEntity SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    OnPropertyChanged("SelectedValue");
                    if (ChangeContentEvent != null)
                    {
                        ChangeContentEvent(new TeamDetailsControl(SelectedItem.Name, ChangeContentEvent));
                    }
                }
            }
        }

        private async void UpdateTeams()
        {
            List<TeamEntity> temp = await GetTeamsAsync();
            Teams = new ObservableCollection<TeamEntity>(temp);
        }

        private async Task<List<TeamEntity>> GetTeamsAsync()
        {
            return await Task.Run(()=> {
                return MainModel.GetTeamDetails();
            });
        }

        private void DeleteTeam(object obj)
        {
           
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
