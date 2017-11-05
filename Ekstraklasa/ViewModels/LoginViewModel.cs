using Oracle.ManagedDataAccess.Client;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
namespace Ekstraklasa
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;
        public delegate void SimpleEventHandler();
        public event SimpleEventHandler OpenMainWindow;
        public event SimpleEventHandler Close;

        public LoginViewModel()
        {
        }

        private ICommand _loginCommand;
        public ICommand loginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand(param => this.Login());
                }
                return _loginCommand;
            }
        }

        private string _Username = null;
        public string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                if (value != _Username)
                {
                    _Username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        private string _Password = null;
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                if (value != _Password)
                {
                    _Password = value;
                    OnPropertyChanged("Password");
                }
            }

        }

        private bool _ShowBadLogin = false;
        public bool ShowBadLogin
        {
            get
            {
                return _ShowBadLogin;
            }
            set
            {
                if (value != _ShowBadLogin)
                {
                    _ShowBadLogin = value;
                    OnPropertyChanged("ShowBadLogin");
                }
            }
        }

        private string _ErrorText = "Błąd";
        public string ErrorText
        {
            get
            {
                return _ErrorText;
            }
            set
            {
                if (value != _ErrorText)
                {
                    _ErrorText = value;
                    OnPropertyChanged("ErrorText");
                }
            }
        }

        private bool _IsLogingFieldEnabled = true;
        public bool IsLogingFieldEnabled
        {
            get
            {
                return _IsLogingFieldEnabled;
            }
            set
            {
                if (value != _IsLogingFieldEnabled)
                {
                    _IsLogingFieldEnabled = value;
                    OnPropertyChanged("IsLogingFieldEnabled");
                }
            }
        }

        private async void Login()
        {
            if (_IsLogingFieldEnabled)
            {
                IsLogingFieldEnabled = false;
                ShowBadLogin = false;
                int result = await ValidateLogin();
                IsLogingFieldEnabled = true;
                if (result != 0)
                {
                    ShowBadLogin = true;
                    switch (result)
                    {
                        case 1: ErrorText = "Niepoprawny login lub hasło"; break;
                        case 2: ErrorText = "Brak połączenia z bazą danych"; break;
                    }
                }
                else
                {
                    ShowBadLogin = false;
                    OnSimpleEvent(OpenMainWindow);
                    OnSimpleEvent(Close);
                }
            }

        }

        private async Task<int> ValidateLogin()
        {
            return await Task.Run(() =>
            {
                return MainModel.ValidateLogin(_Username, _Password);
            });
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void OnSimpleEvent(SimpleEventHandler handler)
        {
            if (handler != null)
            {
                handler();
            }
        }
    }
}
