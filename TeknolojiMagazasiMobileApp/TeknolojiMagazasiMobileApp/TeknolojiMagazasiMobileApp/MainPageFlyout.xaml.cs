using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TeknolojiMagazasiMobileApp.Views;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TeknolojiMagazasiMobileApp.Views.KullanıcıViews;
using TeknolojiMagazasiMobileApp.Views.UrunViews;
using TeknolojiMagazasiMobileApp.Views.MarkaViews;

namespace TeknolojiMagazasiMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageFlyout : ContentPage
    {

        public ListView ListView;

        public MainPageFlyout()
        {
            InitializeComponent();

            BindingContext = new MainPageFlyoutViewModel();
            ListView = MenuItemsList;
        }

        private class MainPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainPageFlyoutMenuItem> MenuItems { get; set; }

            public MainPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<MainPageFlyoutMenuItem>(new[]
                {
                    new MainPageFlyoutMenuItem { Id = 0, Title = "Kullanıcı Listesi", TargetType = typeof(KullanıcıListPage) },
                    new MainPageFlyoutMenuItem { Id = 1, Title = "Ürün Listesi", TargetType = typeof(UrunListPage) },
                    new MainPageFlyoutMenuItem { Id = 1, Title = "Marka Listesi", TargetType = typeof(MarkaListPage) }
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}