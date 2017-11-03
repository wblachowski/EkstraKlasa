using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Ekstraklasa
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;
        public delegate void SimpleEventHandler();
        public event SimpleEventHandler OpenNewWindow;
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

        private void Login()
        {
            System.Diagnostics.Debug.WriteLine(this._Username + " " + this._Password);
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(_Password));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            Console.WriteLine( Sb.ToString());
            ShowBadLogin = true;
            this.OnSimpleEvent(this.Close);
        }

        virtual protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void OnSimpleEvent(SimpleEventHandler handler)
        {
            if(handler != null)
            {
                handler();
            }
        }
    }
}
