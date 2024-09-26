using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeknolojiMagazasiMobileApp.Models;

namespace TeknolojiMagazasiMobileApp.DatabaseManagement
{
    public class MarkaManager
    {
        private readonly DatabaseContext context;

        public MarkaManager(DatabaseContext context)
        {
            this.context = context;
        }

        public Task<List<Marka>> ToListAsync()
        {
            return context.Connection.Table<Marka>().ToListAsync();
        }

        public Task<Marka> GetMarkaAsync(int id)
        {
            return context.Connection.GetAsync<Marka>(id);
            //return context.Connection.Table<Kisi>().Where(x=>x.Id== id).FirstOrDefaultAsync();
        }

        public Task<int> SaveMarkaAsync(Marka marka)
        {
            if (marka.Id == 0)
            {
                return context.Connection.InsertAsync(marka);
            }
            else
            {
                return context.Connection.UpdateAsync(marka);
            }
        }

        public Task<int> DeleteAsync(int id)
        {
            return context.Connection.DeleteAsync<Marka>(id);
        }

        public Task<int> DeleteAsync(Marka kisi)
        {
            return context.Connection.DeleteAsync(kisi);
        }
    }
}