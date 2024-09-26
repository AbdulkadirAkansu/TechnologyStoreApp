using TeknolojiMagazasiMobileApp.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeknolojiMagazasiMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : FlyoutPage
    {

        public MainPage()
        {
            InitializeComponent();

            menuPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainPageFlyoutMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            menuPage.ListView.SelectedItem = null;
        }
    }
}
