﻿using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MatchControl : UserControl
    {
        public MatchControl()
        {
            InitializeComponent();
            var viewModel = new MatchViewModel();
            DataContext = viewModel;
        }

        public MatchControl(MatchEntity match, delegateUpdateControl UpdateContentEvent = null, delegateChangeControl ChangeContentEvent = null, delegateShowSnackbar ShowSnackbarEvent = null)
        {
            InitializeComponent();
            var viewModel = new MatchViewModel(match);
            viewModel.UpdateContentEvent += UpdateContentEvent;
            viewModel.ChangeContentEvent += ChangeContentEvent;
            viewModel.ShowSnackbarEvent += ShowSnackbarEvent;

            DataContext = viewModel;
        }

        private void OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("SAMPLE 2: Closing dialog with parameter: " + (eventArgs.Parameter ?? ""));
        }
    }
}
