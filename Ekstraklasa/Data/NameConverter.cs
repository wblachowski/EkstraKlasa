using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Ekstraklasa
{
    public class NameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                  System.Globalization.CultureInfo culture)
        {

            List<string> list = new List<string>();

            if(value.GetType() == typeof(PlayerEntity))
            {
                return (value as PlayerEntity).Firstname.Substring(0, 1) + '.' + (value as PlayerEntity).Lastname;

            }

            if (value.GetType() == typeof(ObservableCollection<PlayerEntity>))
            {

                foreach (object player in (value as ObservableCollection<PlayerEntity>))
                {
                    if (player != null)
                    {
                        list.Add((player as PlayerEntity).Firstname.Substring(0, 1) + '.' + (player as PlayerEntity).Lastname);
                    }
                    else
                    {
                        list.Add("");
                    }
                }
                return list;
            }
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter,
                         System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
