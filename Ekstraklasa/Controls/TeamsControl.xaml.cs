using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for Teams.xaml
    /// </summary>
    public partial class TeamsControl : UserControl
    {
        public TeamsControl(delegateChangeControl ChangeControl = null, delegateUpdateControl UpdateControl = null, delegateShowSnackbar ShowSnackbar = null)
        {
            InitializeComponent();
            var viewModel = new TeamsViewModel();
            viewModel.ChangeContentEvent += ChangeControl;
            viewModel.UpdateContentEvent += UpdateControl;
            viewModel.ShowSnackbarEvent += ShowSnackbar;
            DataContext = viewModel;
        }

        private void OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("SAMPLE 2: Closing dialog with parameter: " + (eventArgs.Parameter ?? ""));
        }

        private void ListViewSizeChanged(object sender, EventArgs a)
        {
            if (sender != null)
            {
                NameColumn.Width = Math.Max((sender as ListView).ActualWidth - 100, 0);

            }
        }
    }
}
