using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace KelurahanSentani.DataModels
{
    public class KartuKeluargaCollection
    {
        private string uid;

        public KartuKeluargaCollection()
        {

        }

        public KartuKeluargaCollection(string uid)
        {
            this.uid = uid;
        }

        public List<kartukeluarga> GetKartuKeluarga()
        {
            List<kartukeluarga> listKK = new List<kartukeluarga>();
          
            using (var db = new OcphDbContext())
            {
                if (!string.IsNullOrEmpty(this.uid))
                {
                    var pejabatcoll = new Apis.Collections.PejabatCollection();
                    var pejabat = pejabatcoll.GetPejabatByUSerId(uid);
                    if(pejabat!=null)
                    {
                        if(pejabat.Level== LevelStruktur.RT)
                        {
                            var instansi = pejabat.Instansi as rt;
                            listKK = (from kk in db.KartuKeluarga.Select()
                                          join rt in db.RT.Where(o=>o.Id==instansi.Id) on kk.RTId equals rt.Id
                                          join rw in db.RW.Select() on rt.RWId equals rw.Id
                                          select new kartukeluarga
                                          {
                                              Alamat = kk.Alamat,
                                              Id = kk.Id,
                                              NoKK = kk.NoKK,
                                              RTId = kk.RTId,
                                              RW = rw,
                                              RT = rt
                                          }).ToList();
                        }
                        else if(pejabat.Level== LevelStruktur.RW)
                        {
                            var instansi = pejabat.Instansi as rw;
                            listKK = (from kk in db.KartuKeluarga.Select()
                                      join rt in db.RT.Select() on kk.RTId equals rt.Id
                                      join rw in db.RW.Where(o=>o.Id==instansi.Id) on rt.RWId equals rw.Id
                                      select new kartukeluarga
                                      {
                                          Alamat = kk.Alamat,
                                          Id = kk.Id,
                                          NoKK = kk.NoKK,
                                          RTId = kk.RTId,
                                          RW = rw,
                                          RT = rt
                                      }).ToList();
                        }else
                        {
                            listKK = (from kk in db.KartuKeluarga.Select()
                                      join rt in db.RT.Select() on kk.RTId equals rt.Id
                                      join rw in db.RW.Select() on rt.RWId equals rw.Id
                                      select new kartukeluarga
                                      {
                                          Alamat = kk.Alamat,
                                          Id = kk.Id,
                                          NoKK = kk.NoKK,
                                          RTId = kk.RTId,
                                          RW = rw,
                                          RT = rt
                                      }).ToList();
                        }
                    }else
                    {
                        listKK = (from kk in db.KartuKeluarga.Select()
                                  join rt in db.RT.Select() on kk.RTId equals rt.Id
                                  join rw in db.RW.Select() on rt.RWId equals rw.Id
                                  select new kartukeluarga
                                  {
                                      Alamat = kk.Alamat,
                                      Id = kk.Id,
                                      NoKK = kk.NoKK,
                                      RTId = kk.RTId,
                                      RW = rw,
                                      RT = rt
                                  }).ToList();
                    }


                }


                foreach (var item in listKK)
                {
                    List<penduduk> peduduks = new List<penduduk>();
                    foreach (var data in db.KKDetail.Where(O => O.KartuKeluargaId == item.Id))
                    {
                        var p = (from pp in db.Penduduk.Where(O => O.Id == data.PendudukId)
                                 join d in db.PendudukDetail.Select() on pp.Id equals d.Id

                                 select new penduduk
                                 {
                                     Agama = pp.Agama,
                                     Id = pp.Id,
                                     JK = pp.JK,
                                     //  KartuKeluarga = item,
                                     Nama = pp.Nama,
                                     NIK = pp.NIK,
                                     Pekerjaan = pp.Pekerjaan,
                                     Pendidikan = pp.Pendidikan,
                                     TanggalLahir = pp.TanggalLahir,
                                     TempatLahir = pp.TempatLahir,
                                     Detail = d
                                 }).FirstOrDefault();
                        if (p != null)
                        {
                            peduduks.Add(p);
                            if (p.Detail.HubunganDalamKeluarga == Hubungan.KepalaKeluarga)
                            {
                                item.KepalaKeluarga = p;
                            }
                        }

                    }
                    item.DaftarKeluarga = peduduks;
                }

                return listKK;
            }
        }

        public kartukeluarga GetKartuKeluargaByKKId(int id)
        {
            using (var db = new OcphDbContext())
            {
                var listKK = (from kk in db.KartuKeluarga.Where(O => O.Id == id)
                              join rt in db.RT.Select() on kk.RTId equals rt.Id
                              join rw in db.RW.Select() on rt.RWId equals rw.Id
                              select new kartukeluarga
                              {
                                  Alamat = kk.Alamat,
                                  Id = kk.Id,
                                  NoKK = kk.NoKK,
                                  RTId = kk.RTId,
                                  RW = rw,
                                  RT = rt
                              }).ToList();


                foreach (var item in listKK)
                {
                    List<penduduk> peduduks = new List<penduduk>();
                    foreach (var data in db.KKDetail.Where(O => O.KartuKeluargaId == item.Id))
                    {
                        var p = (from pp in db.Penduduk.Where(O => O.Id == data.PendudukId)
                                 join d in db.PendudukDetail.Select() on pp.Id equals d.Id
                                 select new penduduk
                                 {
                                     Agama = pp.Agama,
                                     Id = pp.Id,
                                     JK = pp.JK,
                                     //  KartuKeluarga = item,
                                     Nama = pp.Nama,
                                     NIK = pp.NIK,
                                     Pekerjaan = pp.Pekerjaan,
                                     Pendidikan = pp.Pendidikan,
                                     TanggalLahir = pp.TanggalLahir,
                                     TempatLahir = pp.TempatLahir,
                                     Detail = d
                                 }).FirstOrDefault();
                        if (p != null)
                        {
                            peduduks.Add(p);
                            if (p.Detail.HubunganDalamKeluarga == Hubungan.KepalaKeluarga)
                            {
                                item.KepalaKeluarga = p;
                            }
                        }

                    }
                    item.DaftarKeluarga = peduduks;
                }

                return listKK.FirstOrDefault();
            }
        }

        public kartukeluarga GetKartuKeluargaByNoKK(string Nokk)
        {
            using (var db = new OcphDbContext())
            {
                var listKK = (from kk in db.KartuKeluarga.Where(O => O.NoKK == Nokk)
                              join rt in db.RT.Select() on kk.RTId equals rt.Id
                              join rw in db.RW.Select() on rt.RWId equals rw.Id
                              select new kartukeluarga
                              {
                                  Alamat = kk.Alamat,
                                  Id = kk.Id,
                                  NoKK = kk.NoKK,
                                  RTId = kk.RTId,
                                  RW = rw,
                                  RT = rt
                              }).ToList();


                foreach (var item in listKK)
                {
                    List<penduduk> peduduks = new List<penduduk>();
                    foreach (var data in db.KKDetail.Where(O => O.KartuKeluargaId == item.Id))
                    {
                        var p = (from pp in db.Penduduk.Where(O => O.Id == data.PendudukId)
                                 join d in db.PendudukDetail.Select() on pp.Id equals d.Id
                                 select new penduduk
                                 {
                                     Agama = pp.Agama,
                                     Id = pp.Id,
                                     JK = pp.JK,
                                     //  KartuKeluarga = item,
                                     Nama = pp.Nama,
                                     NIK = pp.NIK,
                                     Pekerjaan = pp.Pekerjaan,
                                     Pendidikan = pp.Pendidikan,
                                     TanggalLahir = pp.TanggalLahir,
                                     TempatLahir = pp.TempatLahir,
                                     Detail = d
                                 }).FirstOrDefault();
                        if (p != null)
                        {
                            peduduks.Add(p);
                            if (p.Detail.HubunganDalamKeluarga == Hubungan.KepalaKeluarga)
                            {
                                item.KepalaKeluarga = p;
                            }
                        }

                    }
                    item.DaftarKeluarga = peduduks;
                }

                return listKK.FirstOrDefault();
            }
        }

        public penduduk GetPendudukById(int id)
        {

            using (var db = new OcphDbContext())
            {
                var p = (from pp in db.Penduduk.Where(O => O.Id == id)
                         join d in db.PendudukDetail.Select() on pp.Id equals d.Id
                         select new penduduk
                         {
                             Agama = pp.Agama,
                             Id = pp.Id,
                             JK = pp.JK,
                             //  KartuKeluarga = item,
                             Nama = pp.Nama,
                             NIK = pp.NIK,
                             Pekerjaan = pp.Pekerjaan,
                             Pendidikan = pp.Pendidikan,
                             TanggalLahir = pp.TanggalLahir,
                             TempatLahir = pp.TempatLahir,
                             Detail = d
                         }).FirstOrDefault();
                var kklist = db.KKDetail.Where(O => O.PendudukId == id).FirstOrDefault();
                if (kklist != null)
                {
                    p.KartuKeluarga = this.GetKartuKeluargaByKKId(kklist.KartuKeluargaId);
                }

                return p;

            }
        }
    }
}