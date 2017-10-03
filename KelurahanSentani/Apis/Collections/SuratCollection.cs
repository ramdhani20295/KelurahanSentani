using KelurahanSentani.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KelurahanSentani.Apis.Collections
{
    public class SuratCollection
    {

        public List<umum> GetSuratUmum()
        {
            using (var db = new OcphDbContext())
            {
                var result = from a in db.Umum.Select()
                             join b in db.Surat.Select() on a.SuratId equals b.Id
                             join c in db.Penduduk.Select() on a.NIK equals c.NIK
                             select new umum { keterangan = a.keterangan, Kop = a.Kop, NIK = a.NIK, Surat = b, SuratId = a.SuratId, Penduduk=c };

                return result.ToList();

            }
        }


    }
}