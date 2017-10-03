using KelurahanSentani.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KelurahanSentani.Apis.Collections
{
    public class PejabatCollection
    {
        public List<pejabat> Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Pejabat.Select();
                foreach (var item in result)
                {
                    if (item.Level == LevelStruktur.RW)
                    {
                        item.Instansi = db.RW.Where(O => O.Id == item.InstansiID).FirstOrDefault();
                    }
                    else if (item.Level == LevelStruktur.RT)
                    {
                        item.Instansi = db.RT.Where(O => O.Id == item.InstansiID).FirstOrDefault();
                    }
                }
               return result.ToList();
            }
        }

        public pejabat GetPejabatById(int id)
        {
            using (var db = new OcphDbContext())
            {
                var item = db.Pejabat.Where(O => O.Id == id).FirstOrDefault();
                if (item != null && item.Level == LevelStruktur.RW)
                {
                    item.Instansi = db.RW.Where(O => O.Id == item.InstansiID).FirstOrDefault();
                }
                else if (item != null && item.Level == LevelStruktur.RT)
                {
                    item.Instansi = db.RT.Where(O => O.Id == item.InstansiID).FirstOrDefault();
                }
                return item;
            }

        }
        public pejabat GetPejabatByUSerId(string id)
        {
            using (var db = new OcphDbContext())
            {
                var item = db.Pejabat.Where(O => O.usersId== id).FirstOrDefault();
                if (item != null && item.Level == LevelStruktur.RW)
                {
                    item.Instansi = db.RW.Where(O => O.Id == item.InstansiID).FirstOrDefault();
                }
                else if (item != null && item.Level == LevelStruktur.RT)
                {
                    item.Instansi = db.RT.Where(O => O.Id == item.InstansiID).FirstOrDefault();
                }
                return item;
            }

        }
    }
}