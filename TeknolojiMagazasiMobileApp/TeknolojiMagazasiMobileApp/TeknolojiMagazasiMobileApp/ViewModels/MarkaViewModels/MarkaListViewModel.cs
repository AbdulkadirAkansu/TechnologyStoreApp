using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TeknolojiMagazasiMobileApp.Models;
using TeknolojiMagazasiMobileApp.ViewModels.Base;
using TeknolojiMagazasiMobileApp.ViewModels.UrunViewModels;
using TeknolojiMagazasiMobileApp.Views.MarkaViews;
using TeknolojiMagazasiMobileApp.Views.UrunViews;
using Xamarin.Forms;

namespace TeknolojiMagazasiMobileApp.ViewModels.MarkaViewModels
{
    public class MarkaListViewModel : BaseViewModel
    {
        private INavigation navigation;
        private ObservableCollection<Marka> _items;
        private bool _isBusy = false;
        public ObservableCollection<Marka> Items
        {
            get { return _items; }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        public Command RefreshCommand { get; set; }
        public Command InsertCommand { get; set; }
        public Command UpdateCommand { get; set; }
        public Command DeleteCommand { get; set; }


        public MarkaListViewModel(INavigation navigation)
        {
            this.navigation = navigation;

            RefreshCommand = new Command(OnRefresh);
            InsertCommand = new Command(OnInsert);
            UpdateCommand = new Command<Marka>(OnUpdate);
            DeleteCommand = new Command<Marka>(OnDelete);

            OnRefresh();
        }

        private async void OnRefresh()
        {
            IsBusy = true;
            Items = new ObservableCollection<Marka>(await App.Database.MarkaManager.ToListAsync());
            IsBusy = false;
        }

        private void OnInsert()
        {
            MessagingCenter.Unsubscribe<MarkaViewModel, Marka>(this, "OnOk");
            MessagingCenter.Subscribe<MarkaViewModel, Marka>(this, "OnOk", async (vm, item) =>
            {
                await navigation.PopModalAsync();
                IsBusy = true;
                await App.Database.MarkaManager.SaveMarkaAsync(item);
                OnRefresh();
            });
            MessagingCenter.Unsubscribe<MarkaViewModel>(this, "OnCancel");
            MessagingCenter.Subscribe<MarkaViewModel>(this, "OnCancel", async (vm) =>
            {
                await navigation.PopModalAsync();
            });


            navigation.PushModalAsync(new MarkaPage { BindingContext = new MarkaViewModel() });
        }

        private void OnUpdate(Marka marka)
        {
            MessagingCenter.Unsubscribe<MarkaViewModel, Marka>(this, "OnOk");
            MessagingCenter.Subscribe<MarkaViewModel, Marka>(this, "OnOk", async (vm, item) =>
            {
                await navigation.PopModalAsync();
                IsBusy = true;
                await App.Database.MarkaManager.SaveMarkaAsync(item);
                OnRefresh();
            });
            MessagingCenter.Unsubscribe<MarkaViewModel>(this, "OnCancel");
            MessagingCenter.Subscribe<MarkaViewModel>(this, "OnCancel", async (vm) =>
            {
                await navigation.PopModalAsync();
            });
            navigation.PushModalAsync(new MarkaPage { BindingContext = new MarkaViewModel(marka) });
        }

        public async void OnDelete(Marka item)
        {
            bool cevap = await App.Current.MainPage.DisplayAlert("Kişi Sil", $"{item.Ad} adlı kişiyi silmek istiyor musunuz?", "Evet", "Hayır");
            if (cevap)
            {
                IsBusy = true;
                await App.Database.MarkaManager.DeleteAsync(item.Id);
                OnRefresh();
            }
        }
    }
}

