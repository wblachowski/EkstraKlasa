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
    /// Interaction logic for NewMatchControl.xaml
    /// </summary>
    public partial class NewMatchControl : UserControl
    {
        public NewMatchControl()
        {
            InitializeComponent();
            var viewModel = new NewMatchViewModel();
            DataContext = viewModel;
        }

        public NewMatchControl(delegateChangeControl changeContentDelegate, delegateUpdateControl updateContentDelegate, delegateShowSnackbar showSnackbarDelegate)
        {
            InitializeComponent();
            var viewModel = new NewMatchViewModel();
            viewModel.ChangeContentEvent += changeContentDelegate;
            viewModel.UpdateContentEvent += updateContentDelegate;
            viewModel.ShowSnackbarEvent += showSnackbarDelegate;

            DataContext = viewModel;
        }

        public NewMatchControl(MatchEntity UpdatedMatch, delegateChangeControl changeContentDelegate, delegateUpdateControl updateContentDelegate, delegateShowSnackbar showSnackbarDelegate)
        {
            InitializeComponent();
            var viewModel = new NewMatchViewModel(UpdatedMatch);
            viewModel.ChangeContentEvent += changeContentDelegate;
            viewModel.UpdateContentEvent += updateContentDelegate;
            viewModel.ShowSnackbarEvent += showSnackbarDelegate;

            DataContext = viewModel;
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}
