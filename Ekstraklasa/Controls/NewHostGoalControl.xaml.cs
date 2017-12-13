using System;
using System.Collections.Generic;
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
    /// Interaction logic for NewGoalControl.xaml
    /// </summary>
    public partial class NewHostGoalControl : UserControl
    {
        public NewHostGoalControl()
        {
            InitializeComponent();
            var viewModel = new GoalViewModel();
            DataContext = viewModel;
        }

        public NewHostGoalControl(GoalEntity goal)
        {
            InitializeComponent();
            var viewModel = new GoalViewModel(goal);
            DataContext = viewModel;
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}
