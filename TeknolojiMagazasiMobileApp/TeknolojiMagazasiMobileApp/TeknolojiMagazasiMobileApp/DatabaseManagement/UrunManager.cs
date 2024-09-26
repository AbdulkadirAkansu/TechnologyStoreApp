using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeknolojiMagazasiMobileApp.Models;

namespace TeknolojiMagazasiMobileApp.DatabaseManagement
{
    public class UrunManager
    {
        private readonly DatabaseContext context;

        public UrunManager(DatabaseContext context)
        {
            this.context = context;
        }

        public Task<List<Urun>> ToListAsync()
        {
            return context.Connection.Table<Urun>().ToListAsync();
        }

        public Task<Urun> GetKisiAsync(int id)
        {
            return context.Connection.GetAsync<Urun>(id);
        }

        public Task<int> SaveKisiAsync(Urun urun)
        {
            if (urun.Id == 0)
            {
                return context.Connection.InsertAsync(urun);
            }
            else
            {
                return context.Connection.UpdateAsync(urun);
            }
        }

        public Task<int> DeleteAsync(int id)
        {
            return context.Connection.DeleteAsync<Urun>(id);
        }

        public Task<int> DeleteAsync(Urun urun)
        {
            return context.Connection.DeleteAsync(urun);
        }
    }
}
