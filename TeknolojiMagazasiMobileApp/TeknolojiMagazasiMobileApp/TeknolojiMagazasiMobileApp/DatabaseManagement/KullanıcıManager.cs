using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeknolojiMagazasiMobileApp.Models;

namespace TeknolojiMagazasiMobileApp.DatabaseManagement
{
    public class KullanıcıManager
    {
        private readonly DatabaseContext context;
        public KullanıcıManager(DatabaseContext context)
        {
            this.context = context;
        }

        public Task<List<Kullanıcı>> GetAllAsync()
        {
            return context.Connection.Table<Kullanıcı>().ToListAsync();
        }

        public Task<Kullanıcı> GetKullanıcıAsync(int id)
        {
            return context.Connection.GetAsync<Kullanıcı>(id);
        }

        public async Task<int> SaveKullanıcıAsync(Kullanıcı user)
        {
            if (user.Id == 0)
            {
                if (await context.Connection.Table<Kullanıcı>().FirstOrDefaultAsync(x => x.KullanıcıAdı.Equals(user.KullanıcıAdı)) != null)
                    throw new Exception($"{user.KullanıcıAdı} kullanıcı adına sahip kullanıcı zaten var...");
                else
                    return await context.Connection.InsertAsync(user);
            }
            else
            {
                if (await context.Connection.Table<Kullanıcı>().FirstOrDefaultAsync(x => x.Id != user.Id && x.KullanıcıAdı.Equals(user.KullanıcıAdı)) != null)
                    throw new Exception($"{user.KullanıcıAdı} kullanıcı adına sahip başka kullanıcı zaten var");
                else
                    return await context.Connection.UpdateAsync(user);
            }
        }

        public async Task<int> DeleteAsync(int id)
        {

            if (await context.Connection.Table<Kullanıcı>().CountAsync() > 1)
                return await context.Connection.DeleteAsync<Kullanıcı>(id);
            else
                throw new Exception("Kayıtlı 1 adet kullanıcı kaldığı için silemezsiniz...");
        }

        public async Task<int> DeleteAsync(Kullanıcı user)
        {
            if (await context.Connection.Table<Kullanıcı>().CountAsync() > 1)
                return await context.Connection.DeleteAsync(user);
            else
                throw new Exception("Kayıtlı 1 adet kullanıcı kaldığı için silemezsiniz...");

        }

        public async Task<bool> Login(Kullanıcı user)
        {
            if (await context.Connection.Table<Kullanıcı>().FirstOrDefaultAsync(x => x.KullanıcıAdı.Equals(user.KullanıcıAdı) &&
                                                                                     x.Parola.Equals(user.Parola)) == null)
                return false;
            else
                return true;
        }
    }
}
