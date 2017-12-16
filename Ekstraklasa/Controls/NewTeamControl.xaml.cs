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

        private void PreviewTextCoach(object sender, TextCompositionEventArgs e)
        {
            (DataContext as NewTeamViewModel).ExecuteCoachDialog(null);
            e.Handled = false;
        }

        private void coach_clicked(Object sender, EventArgs a)
        {
                (DataContext as NewTeamViewModel).ExecuteCoachDialog(null);
        }

        private void open_file_browser(Object sender, EventArgs a)
        {
            System.IO.Stream myStream = null;
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "All files (*.*)|*.*|Image Files(*.PNG,*.BMP;*.JPG;*.GIF)|*.PNG,*.BMP;*.JPG;*.GIF";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
