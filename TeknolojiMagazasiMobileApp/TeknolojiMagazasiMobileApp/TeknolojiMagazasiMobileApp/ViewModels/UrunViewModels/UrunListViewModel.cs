using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TeknolojiMagazasiMobileApp.Models;
using TeknolojiMagazasiMobileApp.ViewModels.Base;
using TeknolojiMagazasiMobileApp.Views.UrunViews;
using Xamarin.Forms;

namespace TeknolojiMagazasiMobileApp.ViewModels.UrunViewModels
{
    public class UrunListViewModel : BaseViewModel
    {
        private INavigation navigation;
        private ObservableCollection<Urun> _items;
        private bool _isBusy = false;
        public ObservableCollection<Urun> Items
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


        public UrunListViewModel(INavigation navigation)
        {
            this.navigation = navigation;

            RefreshCommand = new Command(OnRefresh);
            InsertCommand = new Command(OnInsert);
            UpdateCommand = new Command<Urun>(OnUpdate);
            DeleteCommand = new Command<Urun>(OnDelete);

            OnRefresh();
        }

        private async void OnRefresh()
        {
            IsBusy = true;
            Items = new ObservableCollection<Urun>(await App.Database.UrunManager.ToListAsync());
            IsBusy = false;
        }

        private void OnInsert()
        {
            MessagingCenter.Unsubscribe<UrunViewModel, Urun>(this, "OnOk");
            MessagingCenter.Subscribe<UrunViewModel, Urun>(this, "OnOk", async (vm, item) =>
            {
                await navigation.PopModalAsync();
                IsBusy = true;
                await App.Database.UrunManager.SaveKisiAsync(item);
                OnRefresh();
            });
            MessagingCenter.Unsubscribe<UrunViewModel>(this, "OnCancel");
            MessagingCenter.Subscribe<UrunViewModel>(this, "OnCancel", async (vm) =>
            {
                await navigation.PopModalAsync();
            });


            navigation.PushModalAsync(new UrunPage { BindingContext = new UrunViewModel() });
        }

        private void OnUpdate(Urun urun)
        {
            MessagingCenter.Unsubscribe<UrunViewModel, Urun>(this, "OnOk");
            MessagingCenter.Subscribe<UrunViewModel, Urun>(this, "OnOk", async (vm, item) =>
            {
                await navigation.PopModalAsync();
                IsBusy = true;
                await App.Database.UrunManager.SaveKisiAsync(item);
                OnRefresh();
            });
            MessagingCenter.Unsubscribe<UrunViewModel>(this, "OnCancel");
            MessagingCenter.Subscribe<UrunViewModel>(this, "OnCancel", async (vm) =>
            {
                await navigation.PopModalAsync();
            });
            navigation.PushModalAsync(new UrunPage { BindingContext = new UrunViewModel(urun) });
        }

        public async void OnDelete(Urun urun)
        {
            bool cevap = await App.Current.MainPage.DisplayAlert("Ürün Sil", $"{urun.Ad} adlı ürünü silmek istiyor musunuz?", "Evet", "Hayır");
            if (cevap)
            {
                IsBusy = true;
                await App.Database.UrunManager.DeleteAsync(urun.Id);
                OnRefresh();
            }
        }
    }
}

