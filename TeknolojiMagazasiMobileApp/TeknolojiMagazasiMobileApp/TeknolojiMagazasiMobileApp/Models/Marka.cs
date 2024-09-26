using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeknolojiMagazasiMobileApp.Models
{
    [Table("tblMarkalar")]
    public class Marka
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Ad { get; set; }
  
    }
}
