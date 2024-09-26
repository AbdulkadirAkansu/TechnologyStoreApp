using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeknolojiMagazasiMobileApp.Models
{
    [Table("tblUrunler")]
    public class Urun
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Ad { get; set; }
        public int StokAdet { get; set; }
        public decimal Fiyat { get; set; }
    }
}
