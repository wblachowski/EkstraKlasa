﻿using System;
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
            var viewModel = new MainViewModel();
            DataContext = viewModel;
        }

        public void AdjustOpacity(Object sender, EventArgs e)
        {
            
            foreach (ListBoxItem item in m_listbox_menu.Items)
            {
                StackPanel panel = item.Content as StackPanel;
                Object icon = panel.Children[0];
                if (item.IsSelected)
                {
                    (icon as Control).Opacity = 1;
                }
                else
                {
                    (icon as Control).Opacity = 0.63;
                }
            }
        }
 
    }
}
