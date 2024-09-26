using SQLite;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TeknolojiMagazasiMobileApp.Models;

namespace TeknolojiMagazasiMobileApp.DatabaseManagement
{
    public class DatabaseContext
    {
        private readonly SQLiteAsyncConnection _connection;
        public SQLiteAsyncConnection Connection { get { return _connection; } }
        public DatabaseContext(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);

            Initialize();

        }

        private async void Initialize()
        {
            await _connection.CreateTableAsync<Kullanıcı>();
            await _connection.CreateTableAsync<Marka>();
            await _connection.CreateTableAsync<Urun>();

            if (await _connection.Table<Kullanıcı>().CountAsync() == 0)
            {
                await _connection.InsertAsync(new Kullanıcı
                {
                    KullanıcıAdı = "admin",
                    Parola = "1234"
                });
            }
        }
    }
}
