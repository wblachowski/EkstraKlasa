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
    /// Interaction logic for Matches.xaml
    /// </summary>
    public partial class MatchesControl : UserControl
    {
        public MatchesControl()
        {
            InitializeComponent();
            var viewModel = new MatchesViewModel();
            DataContext = viewModel;
        }

        private void Filter_Mouse_Down(object sender, MouseButtonEventArgs e)
        {
            m_tvFilter.IsExpanded = !m_tvFilter.IsExpanded;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            m_tvFilter.IsExpanded = false;
        }
    }
}
