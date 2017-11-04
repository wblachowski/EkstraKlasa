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
using Ekstraklasa.Controls;

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
                //case 1: m_content_control.Content = new Table(); break;
            }
            if(selection == 1)
            {
                MatchControl match = new MatchControl();
                match.tbTeamA.Text = "Legia warszawa";
                match.tbTeamB.Text = "Lech Poznań";
                match.score.Text = "1:2";
                GoalControl goalControl = new GoalControl();
                goalControl.Minute.Text = "23'";
                goalControl.Scorer.Text = "W.Pastus";
                match.GoalsA.Children.Add(goalControl);
                goalControl = new GoalControl();

                goalControl.Minute.Text = "69'";
                goalControl.Scorer.Text = "M.Mikołajczak";
                match.GoalsA.Children.Add(goalControl);

                GoalControl goalControlR = new GoalControl();

                goalControlR.Minute.Text = "88'";
                goalControlR.Scorer.Text = "Ł.Fabiańskasdasddasi";
                match.GoalsB.Children.Add(goalControlR);

 
                m_content_control.Content = match;
            }
        }
    }
}
