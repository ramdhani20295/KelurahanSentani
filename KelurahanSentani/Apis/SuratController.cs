using KelurahanSentani.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace KelurahanSentani.Apis
{
    public class SuratController : ApiController
    {
        // GET: api/Surat
        public surat Get(int Id,JenisSurat Jenis)
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Surat.Where(O => O.Id == Id).FirstOrDefault();
                if (result != null && Jenis == JenisSurat.Kematian)
                {
                    var data = db.Kematian.Where(O => O.surat_Id == Id).FirstOrDefault();
                    if (data != null)
                        data.Penduduk = new Collections.PendudukCollection().GetPendudukByNIK(data.NIK);
                    result.DataSurat = data;
                }
                else if (result != null && Jenis == JenisSurat.Umum)
                {
                    var data = db.Umum.Where(O => O.SuratId == Id).FirstOrDefault();
                    if (data != null)
                        data.Penduduk = new Collections.PendudukCollection().GetPendudukByNIK(data.NIK);
                    result.DataSurat = data;
                } else if (result != null && Jenis == JenisSurat.Pindah)
                {
                    var data = db.Pindah.Where(O => O.SuratId == Id).FirstOrDefault();
                    if (data != null)
                    {
                        data.Penduduk = new Collections.PendudukCollection().GetPendudukByNIK(data.NIK);
                        data.AnggotaPindah = from a in db.AnggotaPindah.Where(O => O.surat_id == data.SuratId)
                                             join b in db.Penduduk.Select() on a.NIK equals b.NIK
                                             select new anggotapindah
                                             {
                                                 idAnggotaPindah = a.idAnggotaPindah,
                                                 NIK = a.NIK,
                                                 Penduduk = b,
                                                 PermohonanId = a.PermohonanId,
                                                 surat_id = a.surat_id
                                             };
                        result.DataSurat = data;
                    }

                } else
                    throw new SystemException("Data Tidak Ditemukan");

                return result;
            }
        }

        // GET: api/Surat/5
       

        // POST: api/Surat
        public void Post([FromBody]string value)
        {
        }

        [HttpGet]
        public HttpResponseMessage Umum()
        {
            try
            {
                var coll = new Collections.SuratCollection();
                return Request.CreateResponse(HttpStatusCode.OK, coll.GetSuratUmum());
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
          
        }

        [HttpPost]
        public HttpResponseMessage Umum(umum umum)
        {
            if(User.IsInRole("Administrator"))
            {
                umum.Surat.AdminUserId = User.Identity.GetUserId();
                using (var db = new OcphDbContext())
                {
                    var trans = db.Connection.BeginTransaction();
                    try
                    {
                        var mohonan = db.Permohonan.Where(O => O.Id == umum.Surat.PermohonanId).FirstOrDefault();
                        if (mohonan != null)
                        {
                            mohonan.Status = StatusPermohonan.Selesai;
                            umum.Surat.Id = db.Surat.InsertAndGetLastID(umum.Surat);
                            if (umum.Surat.Id > 0)
                            {
                                umum.SuratId = umum.Surat.Id;
                                if (db.Umum.Insert(umum) && db.Permohonan.Update(O => new { O.Status }, mohonan, O => O.Id == mohonan.Id))
                                {
                                    trans.Commit();
                                    return Request.CreateResponse(HttpStatusCode.OK, umum);
                                }
                                else
                                    throw new SystemException("Data gagal ditambah");
                            }
                            else
                                throw new SystemException("Data gagal ditambah");
                        }else
                        {
                            throw new SystemException("Data permohonan tidak ditemukan");
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                    }

                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Anda Tidak Memiliki Akses");
            }
        
        }

        [HttpGet]
        public HttpResponseMessage Pindah()
        {
            try
            {
                var coll = new Collections.SuratCollection();
                return Request.CreateResponse(HttpStatusCode.OK, coll.GetSuratPindah());
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpPost]
        public HttpResponseMessage Pindah(pindah pindah)
        {
            if (User.IsInRole("Administrator"))
            {
                pindah.Surat.AdminUserId = User.Identity.GetUserId();
                using (var db = new OcphDbContext())
                {
                    var trans = db.Connection.BeginTransaction();
                    try
                    {
                        var mohonan = db.Permohonan.Where(O => O.Id == pindah.Surat.PermohonanId).FirstOrDefault();
                        if (mohonan != null)
                        {
                            mohonan.Status = StatusPermohonan.Selesai;
                            pindah.Surat.Id = db.Surat.InsertAndGetLastID(pindah.Surat);
                            if (pindah.Surat.Id > 0)
                            {
                                pindah.SuratId = pindah.Surat.Id;
                                if (db.Pindah.Insert(pindah) && db.Permohonan.Update(O => new { O.Status }, mohonan, O => O.Id == mohonan.Id))
                                {
                                    db.AnggotaPindah.Update(O => new { O.surat_id }, new anggotapindah { surat_id = pindah.SuratId }, O => O.PermohonanId == mohonan.Id);
                                    trans.Commit();
                                    return Request.CreateResponse(HttpStatusCode.OK, pindah);
                                }
                                else
                                    throw new SystemException("Data gagal ditambah");
                            }
                            else
                                throw new SystemException("Data gagal ditambah");
                        }
                        else
                        {
                            throw new SystemException("Data permohonan tidak ditemukan");
                        }

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                    }

                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Anda Tidak Memiliki Akses");
            }

        }


        [HttpGet]
        public HttpResponseMessage Kematian()
        {
            try
            {
                var coll = new Collections.SuratCollection();
                return Request.CreateResponse(HttpStatusCode.OK, coll.GetSuratKematian());
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpPost]
        public HttpResponseMessage Kematian(kematian kematian)
        {
            if (User.IsInRole("Administrator"))
            {
                kematian.Surat.AdminUserId = User.Identity.GetUserId();
                using (var db = new OcphDbContext())
                {
                    var trans = db.Connection.BeginTransaction();
                    try
                    {
                        var mohonan = db.Permohonan.Where(O => O.Id == kematian.Surat.PermohonanId).FirstOrDefault();
                        if (mohonan != null)
                        {
                            mohonan.Status = StatusPermohonan.Selesai;
                            kematian.Surat.Id = db.Surat.InsertAndGetLastID(kematian.Surat);
                            if (kematian.Surat.Id > 0)
                            {
                                kematian.surat_Id = kematian.Surat.Id;
                                if (db.Kematian.Insert(kematian) && db.Permohonan.Update(O => new { O.Status }, mohonan, O => O.Id == mohonan.Id))
                                {
                                    trans.Commit();
                                    return Request.CreateResponse(HttpStatusCode.OK, kematian);
                                }
                                else
                                    throw new SystemException("Data gagal ditambah");
                            }
                            else
                                throw new SystemException("Data gagal ditambah");
                        }
                        else
                        {
                            throw new SystemException("Data permohonan tidak ditemukan");
                        }

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                    }

                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Anda Tidak Memiliki Akses");
            }

        }

    }
}
