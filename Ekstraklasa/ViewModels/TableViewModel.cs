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

        private ObservableCollection<TableEntity> _TableEntities;
        public ObservableCollection<TableEntity> TableEntities
        {
            get
            {
                return _TableEntities;
            }
            set
            {
                if (!TableEntities.Equals(value))
                {
                    TableEntities = value;
                    OnPropertyChanged("TableEntities");
                }
            }
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
