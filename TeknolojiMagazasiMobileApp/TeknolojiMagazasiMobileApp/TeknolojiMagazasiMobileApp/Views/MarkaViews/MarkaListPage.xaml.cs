using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeknolojiMagazasiMobileApp.ViewModels.UrunViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeknolojiMagazasiMobileApp.Views.MarkaViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarkaListPage : ContentPage
    {
        public MarkaListPage()
        {
            InitializeComponent();
            BindingContext = new UrunListViewModel(Navigation);
        }
    }
}