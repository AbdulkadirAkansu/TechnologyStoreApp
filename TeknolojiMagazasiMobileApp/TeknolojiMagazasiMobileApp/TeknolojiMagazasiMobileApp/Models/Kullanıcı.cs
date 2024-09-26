using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeknolojiMagazasiMobileApp.Models
{
    [Table("tblKullanıcılar")]
    public class Kullanıcı : BaseModel
    {
        public string KullanıcıAdı { get; set; }
        public string Parola { get; set; }
    }
}
