using TeknolojiMagazasiMobileApp;
using System;
using System.Collections.Generic;
using System.Text;
using TeknolojiMagazasiMobileApp.Models;
using TeknolojiMagazasiMobileApp.ViewModels.Base;
using Xamarin.Forms;

namespace TeknolojiMagazasiMobileApp.ViewModels.KullanıcıViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private Kullanıcı _model;
        private string _errorMessage = "";
        public Kullanıcı Model { get { return _model; } }


        public string KullanıcıAdı
        {
            get { return _model.KullanıcıAdı; }
            set
            {
                if (_model.KullanıcıAdı != value)
                {
                    _model.KullanıcıAdı = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Parola
        {
            get { return _model.Parola; }
            set
            {
                if (_model.Parola != value)
                {
                    _model.Parola = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public Command LoginCommand { get; set; }

        public LoginViewModel()
        {
            _model = new Kullanıcı();
            LoginCommand = new Command(OnLogin);
        }

        private async void OnLogin()
        {
            if (await App.Database.KullanıcıManager.Login(_model))
            {
                ErrorMessage = "";
                App.User = _model;
                App.Current.MainPage = new MainPage();
            }
            else
            {
                ErrorMessage = "Kullanıcı adı ya da parola hatalı!!!";
            }
        }

    }
}
