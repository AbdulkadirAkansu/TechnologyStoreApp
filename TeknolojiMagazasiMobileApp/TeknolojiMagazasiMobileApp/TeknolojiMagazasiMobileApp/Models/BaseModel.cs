using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeknolojiMagazasiMobileApp.Models
{
    public class BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
