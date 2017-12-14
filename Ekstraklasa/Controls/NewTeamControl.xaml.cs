﻿using System;
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
    /// Interaction logic for NewTeamControl.xaml
    /// </summary>
    public partial class NewTeamControl : UserControl
    {
        public NewTeamControl(delegateChangeControl ChangeContent = null)
        {
            InitializeComponent();
            var viewModel = new NewTeamViewModel();
            viewModel.ChangeContentEvent += ChangeContent; 
            DataContext = viewModel;
        }
    }
}
