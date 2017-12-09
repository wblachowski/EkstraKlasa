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
    /// Interaction logic for DeleteMatchDialog.xaml
    /// </summary>
    public partial class DeleteMatchDialog : UserControl
    {
        public DeleteMatchDialog()
        {
            InitializeComponent();
        }

        public DeleteMatchDialog(string message)
        {
            InitializeComponent();
            details.Text = message;
        }
    }
}
