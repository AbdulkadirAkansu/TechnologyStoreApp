using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TeknolojiMagazasiMobileApp.Models;
using TeknolojiMagazasiMobileApp.ViewModels.Base;
using TeknolojiMagazasiMobileApp.Views.KullanıcıViews;
using Xamarin.Forms;

namespace TeknolojiMagazasiMobileApp.ViewModels.KullanıcıViewModels
{
    public class KullanıcıListViewModel : BaseViewModel
    {
        private INavigation navigation;
        private ObservableCollection<Kullanıcı> _items;
        private bool _isBusy = false;

        public ObservableCollection<Kullanıcı> Items
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

        public KullanıcıListViewModel(INavigation navigation)
        {
            this.navigation = navigation;

            RefreshCommand = new Command(OnRefresh);
            InsertCommand = new Command(OnInsert);
            UpdateCommand = new Command<Kullanıcı>(OnUpdate);
            DeleteCommand = new Command<Kullanıcı>(OnDelete);

            OnRefresh();
        }


        private async void OnRefresh()
        {
            IsBusy = true;
            Items = new ObservableCollection<Kullanıcı>(await App.Database.KullanıcıManager.GetAllAsync());
            IsBusy = false;
        }

        private void OnInsert()
        {
            MessagingCenter.Unsubscribe<KullanıcıViewModel, Kullanıcı>(this, "OnOk");
            MessagingCenter.Subscribe<KullanıcıViewModel, Kullanıcı>(this, "OnOk", async (vm, item) =>
            {
                await navigation.PopModalAsync();  
                OnRefresh();
            });

            MessagingCenter.Unsubscribe<KullanıcıViewModel>(this, "OnCancel");
            MessagingCenter.Subscribe<KullanıcıViewModel>(this, "OnCancel", async (vm) =>
            {
                await navigation.PopModalAsync();
            });

            navigation.PushModalAsync(new KullanıcıPage() { BindingContext = new KullanıcıViewModel() });
        }

        private void OnUpdate(Kullanıcı kullanıcı)
        {
            MessagingCenter.Unsubscribe<KullanıcıViewModel, Kullanıcı>(this, "OnOk");
            MessagingCenter.Subscribe<KullanıcıViewModel, Kullanıcı>(this, "OnOk", async (vm, item) =>
            {
                await navigation.PopModalAsync();
                OnRefresh();
            });

            MessagingCenter.Unsubscribe<KullanıcıViewModel>(this, "OnCancel");
            MessagingCenter.Subscribe<KullanıcıViewModel>(this, "OnCancel", async (vm) =>
            {
                await navigation.PopModalAsync();
            });

            navigation.PushModalAsync(new KullanıcıPage() { BindingContext = new KullanıcıViewModel(kullanıcı) });
        }

        private async void OnDelete(Kullanıcı item)
        {
            bool cevap = await App.Current.MainPage.DisplayAlert("Kullanıcı Sil", $"{item.KullanıcıAdı} adlı kullanıcıyı silmek istiyor musunuz?", "Evet", "Hayır");
            if (cevap)
            {
                IsBusy = true;
                try
                {
                    await App.Database.KullanıcıManager.DeleteAsync(item.Id);
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Uyarı", ex.Message, "Tamam");
                }
                OnRefresh();
            }
        }

    }
}
