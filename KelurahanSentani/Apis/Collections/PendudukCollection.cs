using KelurahanSentani.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KelurahanSentani.Apis.Collections
{
    public class PendudukCollection
    {

        internal List<penduduk> Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Penduduk.Select();
                foreach (var item in result)
                {
                    var kkdetail = db.KKDetail.Where(O => O.PendudukId == item.Id).FirstOrDefault();
                    if (kkdetail != null)
                        item.KartuKeluarga = (from a in db.KartuKeluarga.Where(O => O.Id == kkdetail.KartuKeluargaId)
                                              join rt in db.RT.Select() on a.RTId equals rt.Id
                                              join rw in db.RW.Select() on rt.RWId equals rw.Id
                                              select new kartukeluarga
                                              {
                                                  Alamat = a.Alamat,
                                                  DaftarKeluarga = a.DaftarKeluarga,
                                                  Id = a.Id,
                                                  KepalaKeluarga = a.KepalaKeluarga,
                                                  NoKK = a.NoKK,
                                                  RT = rt,
                                                  RTId = a.RTId,
                                                  RW = rw,
                                                  Tanggal = a.Tanggal
                                              }).FirstOrDefault();
                }
                return result.ToList();
            }
        }


        internal penduduk GetPendudukByNIK(string nIK)
        {
            using (var db = new OcphDbContext())
            {
                var result = (from a in db.Penduduk.Where(O => O.NIK == nIK)
                              join b in db.PendudukDetail.Select() on a.Id equals b.Id
                              select new penduduk
                              {
                                  Agama = a.Agama,
                                  Detail = b,
                                  Id = a.Id,
                                  JK = a.JK,
                                  Nama = a.Nama,
                                  NIK = a.NIK,
                                  Pekerjaan = a.Pekerjaan,
                                  Pendidikan = a.Pendidikan,
                                  TanggalLahir = a.TanggalLahir,
                                  TempatLahir = a.TempatLahir
                              }).FirstOrDefault();

                if (result != null)
                {
                    var kkdetail = db.KKDetail.Where(O => O.PendudukId == result.Id).FirstOrDefault();
                    if (kkdetail != null)
                        result.KartuKeluarga = db.KartuKeluarga.Where(O => O.Id == kkdetail.KartuKeluargaId).FirstOrDefault();
                }
                return result;
            }
        }
    }
}