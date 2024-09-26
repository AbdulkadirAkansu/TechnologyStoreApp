using System;
using System.Collections.Generic;
using System.Text;
using TeknolojiMagazasiMobileApp.Models;
using TeknolojiMagazasiMobileApp.ViewModels.Base;
using Xamarin.Forms;

namespace TeknolojiMagazasiMobileApp.ViewModels.KullanıcıViewModels
{
    public class KullanıcıViewModel : BaseViewModel
    {
        private string _parolaTekrar = "";
        private Kullanıcı model;

        public Kullanıcı Model { get { return model; } }

        public int Id
        {
            get { return model.Id; }
            set
            {
                if (model.Id != value)
                {
                    model.Id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string KullanıcıAdı
        {
            get { return model.KullanıcıAdı; }
            set
            {
                if (model.KullanıcıAdı != value)
                {
                    model.KullanıcıAdı = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Parola
        {
            get { return model.Parola; }
            set
            {
                if (model.Parola != value)
                {
                    model.Parola = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ParolaTekrar
        {
            get { return _parolaTekrar; }
            set
            {
                if (_parolaTekrar != value)
                {
                    _parolaTekrar = value;
                    OnPropertyChanged();
                }
            }
        }

        public Command OkCommand { get; set; }
        public Command CancelCommand { get; set; }

        public KullanıcıViewModel() : this(new Kullanıcı()) { }
        public KullanıcıViewModel(Kullanıcı item)
        {
            model = item;
            OkCommand = new Command(OnOk);
            CancelCommand = new Command(OnCancel);
        }

        private async void OnOk()
        {
            if (model.Parola.Equals(_parolaTekrar))
            {
                try
                {
                    await App.Database.KullanıcıManager.SaveKullanıcıAsync(model);
                    MessagingCenter.Send<KullanıcıViewModel, Kullanıcı>(this, "OnOk", model);
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Uyarı", ex.Message, "Tamam");
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("Uyarı", "Parola ile parola tekrarı uyuşmuyor...", "Tamam");
        }

        private void OnCancel()
        {
            MessagingCenter.Send<KullanıcıViewModel>(this, "OnCancel");
        }
    }
}
