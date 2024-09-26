using System;
using System.Collections.Generic;
using System.Text;
using TeknolojiMagazasiMobileApp.Models;
using TeknolojiMagazasiMobileApp.ViewModels.Base;
using Xamarin.Forms;

namespace TeknolojiMagazasiMobileApp.ViewModels.UrunViewModels
{
    public class UrunViewModel : BaseViewModel
    {
        private Urun model;
        public Urun Model { get { return model; } }

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

        public string Ad
        {
            get { return model.Ad; }
            set
            {
                if (model.Ad != value)
                {
                    model.Ad = value;
                    OnPropertyChanged();
                }
            }
        }

        public int StokAdet
        {
            get { return model.StokAdet; }
            set
            {
                if (model.StokAdet != value)
                {
                    model.StokAdet = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal Fiyat
        {
            get { return model.Fiyat; }
            set
            {
                if (model.Fiyat != value)
                {
                    model.Fiyat = value;
                    OnPropertyChanged();
                }
            }
        }

        public Command OkCommand { get; set; }
        public Command CancelCommand { get; set; }

        public UrunViewModel() : this(new Urun()) { }

        public UrunViewModel(Urun item)
        {
            model = item;
            OkCommand = new Command(OnOk);
            CancelCommand = new Command(OnCancel);
        }

        private void OnOk()
        {
            MessagingCenter.Send<UrunViewModel, Urun>(this, "OnOk", model);
        }

        private void OnCancel()
        {
            MessagingCenter.Send<UrunViewModel>(this, "OnCancel");
        }
    }
}
