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

        public List<pindah> GetSuratPindah()
        {
            using (var db = new OcphDbContext())
            {
                var result = from a in db.Pindah.Select()
                             join b in db.Surat.Select() on a.SuratId equals b.Id
                             join c in db.Penduduk.Select() on a.NIK equals c.NIK
                             join d in db.KKDetail.Select() on c.Id equals d.PendudukId
                             join f in db.KartuKeluarga.Select() on d.KartuKeluargaId equals f.Id
                             select new pindah { NIK = a.NIK,   Surat = b, SuratId = a.SuratId,  Alamatbaru=a.Alamatbaru, NoKK=a.NoKK,Penduduk=c, KartuKeluarga=f};

                return result.ToList();
            }
        }


        public List<kematian> GetSuratKematian()
        {
            using (var db = new OcphDbContext())
            {
                var result = from a in db.Kematian.Select()
                             join b in db.Surat.Select() on a.surat_Id equals b.Id
                             join c in db.Penduduk.Select() on a.NIK equals c.NIK
                             select new kematian { NIK = a.NIK, Surat = b,   sebabkematian=a.sebabkematian, surat_Id=a.surat_Id, tglkematian=a.tglkematian,
                              tmptkematian=a.tmptkematian, Penduduk=c};

                return result.ToList();
            }
        }


    }
}