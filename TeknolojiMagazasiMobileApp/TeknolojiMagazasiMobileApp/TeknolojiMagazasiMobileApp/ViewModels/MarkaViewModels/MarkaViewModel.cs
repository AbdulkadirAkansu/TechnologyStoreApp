using System;
using System.Collections.Generic;
using System.Text;
using TeknolojiMagazasiMobileApp.Models;
using TeknolojiMagazasiMobileApp.ViewModels.Base;
using TeknolojiMagazasiMobileApp.ViewModels.UrunViewModels;
using Xamarin.Forms;

namespace TeknolojiMagazasiMobileApp.ViewModels.MarkaViewModels
{
    public class MarkaViewModel : BaseViewModel
    {
        private Marka model;
        public Marka Model { get { return model; } }


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

       
        public Command OkCommand { get; set; }
        public Command CancelCommand { get; set; }

        public MarkaViewModel() : this(new Marka()) { }

        public MarkaViewModel(Marka item)
        {
            model = item;
            OkCommand = new Command(OnOk);
            CancelCommand = new Command(OnCancel);
        }

        private void OnOk()
        {
            MessagingCenter.Send<MarkaViewModel, Marka>(this, "OnOk", model);
        }

        private void OnCancel()
        {
            MessagingCenter.Send<MarkaViewModel>(this, "OnCancel");
        }
    }
}

