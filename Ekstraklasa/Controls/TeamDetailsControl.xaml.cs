using OxyPlot.Axes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ekstraklasa
{ 
    /// <summary>
    /// Interaction logic for TeamDetailsControl.xaml
    /// </summary>
    public partial class TeamDetailsControl : UserControl
    {
        public TeamDetailsControl(string Name="", delegateChangeControl ChangeControl =null)
        {
            InitializeComponent();
            var viewModel = new TeamDetailsViewModel(Name, ChangeControl);
            DataContext = viewModel;
        }

        private string formatter(double d)
        {
            DateTime myDate = DateTimeAxis.ToDateTime(d);
            return myDate.ToString("dd MMM", new CultureInfo("pl-PL"));
        }
    }
}
