using System;
using System.IO;
using TeknolojiMagazasiMobileApp.DatabaseManagement;
using TeknolojiMagazasiMobileApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeknolojiMagazasiMobileApp
{
    public partial class App : Application
    {
        private static UnitOfWork _database;
        private static Kullanıcı _user = null;

        public static UnitOfWork Database
        {
            get
            {
                if (_database == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TeknolojiMagazasi.db3");
                    _database = new UnitOfWork(dbPath);
                }
                return _database;
            }
        }

        public static Kullanıcı User
        {
            get { return _user; }
            set { _user = value; }
        }



        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
