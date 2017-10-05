using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using KelurahanSentani.DataModels;

namespace KelurahanSentani.Apis
{
    public class PermohonanController : ApiController
    {

        [Authorize]
        // GET: api/Permohonan
        public HttpResponseMessage Get()
        {
            var userid = User.Identity.GetUserId();
            try
            {
                using (var db = new OcphDbContext())
                {
                    var pejabat = db.Pejabat.Where(o => o.usersId == userid).FirstOrDefault();
                    List<permohonan> result = new List<permohonan>();
                    if (pejabat != null)
                    {
                        if (pejabat.Level == LevelStruktur.RW)
                        {
                            result = (from a in db.RW.Where(O => O.PejabatId == pejabat.Id)
                                      join b in db.RT.Select() on a.Id equals b.RWId
                                      join c in db.Permohonan.Select() on b.Id equals c.RTId
                                      join d in db.Penduduk.Select() on c.PendudukId equals d.Id
                                      select new permohonan
                                      {
                                          Id = c.Id, EmailPemohon=c.EmailPemohon,
                                          Isi = c.Isi,
                                          PendudukId = c.PendudukId,
                                          JenisSurat = c.JenisSurat,
                                          NomorPermohonan = c.NomorPermohonan,
                                          RTId = c.RTId,
                                          Status = c.Status,
                                          Tanggal = c.Tanggal,
                                          Penduduk = d
                                      }).ToList();

                        }
                        else if (pejabat.Level == LevelStruktur.RT)
                        {
                            result = (from a in db.RT.Where(O => O.PejabatId == pejabat.Id)
                                      join c in db.Permohonan.Select() on a.Id equals c.RTId
                                      join d in db.Penduduk.Select() on c.PendudukId equals d.Id
                                      select new permohonan
                                      {
                                          Id = c.Id,
                                          EmailPemohon = c.EmailPemohon,
                                          Isi = c.Isi,
                                          PendudukId = c.PendudukId,
                                          JenisSurat = c.JenisSurat,
                                          NomorPermohonan = c.NomorPermohonan,
                                          RTId = c.RTId,
                                          Status = c.Status,
                                          Tanggal = c.Tanggal,
                                          Penduduk = d
                                      }).ToList();

                        }
                        else
                        {
                            result = (from c in db.Permohonan.Select()
                                      join d in db.Penduduk.Select() on c.PendudukId equals d.Id
                                      select new permohonan
                                      {
                                          Id = c.Id,
                                          EmailPemohon = c.EmailPemohon,
                                          Isi = c.Isi,
                                          PendudukId = c.PendudukId,
                                          JenisSurat = c.JenisSurat,
                                          NomorPermohonan = c.NomorPermohonan,
                                          RTId = c.RTId,
                                          Status = c.Status,
                                          Tanggal = c.Tanggal,
                                          Penduduk = d
                                      }).ToList();

                        }
                        foreach (var item in result)
                        {
                            if(item.Status== StatusPermohonan.Menunggu)
                            item.StatusPersetujuan = ApiHelper.CekPersetujuan(item.Id, pejabat.Level);
                            else if(item.Status== StatusPermohonan.Selesai)
                            {
                                item.StatusPersetujuan = new PersetujuanCompleted { IAproved = true, IsCompleted = true };
                            }
                        }

                    }
                    else
                    {
                        result = (from c in db.Permohonan.Select()
                                  join d in db.Penduduk.Select() on c.PendudukId equals d.Id
                                  select new permohonan
                                  {
                                      Id = c.Id,
                                      EmailPemohon = c.EmailPemohon,
                                      Isi = c.Isi,
                                      PendudukId = c.PendudukId,
                                      JenisSurat = c.JenisSurat,
                                      NomorPermohonan = c.NomorPermohonan,
                                      RTId = c.RTId,
                                      Status = c.Status,
                                      Tanggal = c.Tanggal,
                                      Penduduk = d
                                  }).ToList();
                        foreach (var item in result)
                        {
                            if (item.Status == StatusPermohonan.Menunggu)
                                item.StatusPersetujuan = ApiHelper.CekPersetujuan(item.Id);
                            else if (item.Status == StatusPermohonan.Selesai)
                            {
                                item.StatusPersetujuan = new PersetujuanCompleted { IAproved = true, IsCompleted = true };
                            }
                        }
                    }

                  

                    return Request.CreateResponse(HttpStatusCode.OK, result.OrderByDescending(O=>O.Tanggal).ToList());
                }
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
           

        }

        // GET: api/Permohonan/5
        public string Get(int id)
        {
            return "value";
        }

        [Authorize(Roles ="RT")]
        // POST: api/Permohonan
        public HttpResponseMessage Post(permohonan data)
        {

            using (var db = new OcphDbContext())
            {
                try
                {
                    data.Tanggal = DateTime.Now;
                    data.Id= db.Permohonan.InsertAndGetLastID(data);
                    if(data.Id>0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }else
                        return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Data gagal ditambah");

                }
                catch (Exception ex)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
        }

        // PUT: api/Permohonan/5
        public HttpResponseMessage Put(permohonan data)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                   if( db.Permohonan.Update(O=>new {O.Isi, O.JenisSurat,O.NomorPermohonan,O.PendudukId,O.EmailPemohon  },data,O=>O.Id==data.Id))
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                   else
                        return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Data gagal ditambah");

                }
                catch (Exception ex)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
        }

        // DELETE: api/Permohonan/5
        public HttpResponseMessage Delete(int id)
        {

            using (var db = new OcphDbContext())
            {
                try
                {
                    if (db.Permohonan.Delete( O => O.Id == id))
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Berhasil Dihapus");
                    }
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Data gagal ditambah");

                }
                catch (Exception ex)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
        }
    }
}
