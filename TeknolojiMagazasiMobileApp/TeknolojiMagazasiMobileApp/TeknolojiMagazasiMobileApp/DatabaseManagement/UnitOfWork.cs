using System;
using System.Collections.Generic;
using System.Text;

namespace TeknolojiMagazasiMobileApp.DatabaseManagement
{
    public class UnitOfWork
    {
        private readonly DatabaseContext context;

        private UrunManager _urunManager;
        private MarkaManager _markaManager;
        private KullanıcıManager _kullanıcıManager;

        public UrunManager UrunManager
        {
            get
            {
                if (_urunManager == null)
                    _urunManager = new UrunManager(context);
                return _urunManager;
            }
        }

        public MarkaManager MarkaManager
        {
            get
            {
                if (_markaManager == null)
                    _markaManager = new MarkaManager(context);
                return _markaManager;
            }
        }

        public KullanıcıManager KullanıcıManager
        {
            get
            {
                if (_kullanıcıManager == null)
                    _kullanıcıManager = new KullanıcıManager(context);
                return _kullanıcıManager;
            }
        }


        public UnitOfWork(string dbPath)
        {
            context = new DatabaseContext(dbPath);
        }
    }
}
