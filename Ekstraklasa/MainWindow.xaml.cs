using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            m_listbox_menu.SelectedIndex = 0;
            m_content_control.Content = new Table();
        }
        void SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            int selection = (sender as ListBox).SelectedIndex;
            switch (selection)
            {
                case 0: m_content_control.Content = new Matches(); break;
                case 1: m_content_control.Content = new Table(); break;
            }
        }
    }
}
