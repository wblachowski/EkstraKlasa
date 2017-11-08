using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekstraklasa
{
    class TableViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;

        public TableViewModel()
        {
            UpdateTable();
        }

        private ObservableCollection<TableEntity> _TableEntities = new ObservableCollection<TableEntity>();
        public ObservableCollection<TableEntity> TableEntities
        {
            get
            {
                return _TableEntities;
            }
            set
            {
                if (!_TableEntities.Equals(value))
                {
                    _TableEntities = value;
                    OnPropertyChanged("TableEntities");
                }
            }
        }

        private bool _IsProgressBarVisible = true;
        public bool IsProgressBarVisible
        {
            get
            {
                return _IsProgressBarVisible;
            }
            set
            {
                if(_IsProgressBarVisible != value)
                {
                    _IsProgressBarVisible = value;
                    OnPropertyChanged("IsProgressBarVisible");
                }
            }
        }

        private async void UpdateTable()
        {
            List<TableEntity> list = await GetCurrentTableAsync();
            TableEntities = new ObservableCollection<TableEntity>(list);
            IsProgressBarVisible = false;

        }

        private async Task<List<TableEntity>> GetCurrentTableAsync()
        {
            return await Task.Run(() => {
                return MainModel.GetCurrentTable();
            });
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
