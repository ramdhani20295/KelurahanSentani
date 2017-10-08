using KelurahanSentani.DataModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KelurahanSentani.Apis
{
    public class KartuKeluargaController : ApiController
    {
        // GET: api/KartuKeluarga
        [Authorize]
        public IEnumerable<kartukeluarga> Get()
        {
            var uid = User.Identity.GetUserId();
            KartuKeluargaCollection collection = new KartuKeluargaCollection(uid);
            return collection.GetKartuKeluarga();
        }

        // GET: api/KartuKeluarga/5
        public HttpResponseMessage Get(string NKK)
        {
           using(var db = new OcphDbContext())
            {
                var kk = db.KartuKeluarga.Where(O => O.NoKK==NKK).FirstOrDefault();
                if(kk!=null)
                {
                    List<penduduk> peduduks = new List<penduduk>();
                    foreach (var data in db.KKDetail.Where(O => O.KartuKeluargaId == kk.Id))
                    {
                        var p = db.Penduduk.Where(O => O.Id == data.PendudukId).FirstOrDefault();
                        if (p != null)
                            peduduks.Add(p);
                    }
                    kk.DaftarKeluarga = peduduks;
                  
                }else
                {
                   
                }
            }
            KartuKeluargaCollection collection = new KartuKeluargaCollection();
            var result = collection.GetKartuKeluargaByNoKK(NKK);
            if(result!=null)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data Tidak Ditemukan");
        }

        // POST: api/KartuKeluarga
        public HttpResponseMessage Post(kartukeluarga value)
        {
            using(var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    value.Tanggal = DateTime.Now;
                    value.Id = db.KartuKeluarga.InsertAndGetLastID(value);
                    if(value.DaftarKeluarga!=null && value.DaftarKeluarga.Count>0)
                    {
                        foreach(var item in value.DaftarKeluarga)
                        {
                            if(item.Id==0)
                            {
                                item.Id = db.Penduduk.InsertAndGetLastID(item);
                                item.Detail = new pendudukdetail { Id = item.Id, HubunganDalamKeluarga = Hubungan.KepalaKeluarga , StatusPerkawinan=StatusPerkawinan.Kawin };
                                value.KepalaKeluarga = item;
                                db.PendudukDetail.InsertAndGetLastID(item.Detail);
                               var res= db.KKDetail.Insert(new kkdetail { KartuKeluargaId = value.Id, PendudukId = item.Id });
                                if(!res)
                                    throw new SystemException("Data Gagal Ditambah");
                            }
                            else
                            {
                                throw new SystemException("Data Gagal Ditambah");
                            }
                        }
                    }else
                    {
                        throw new SystemException("Data Gagal Ditambah");
                    }

                    trans.Commit();
                    return Request.CreateResponse(HttpStatusCode.OK, value);
                }
                catch (Exception ex)
                {

                    trans.Rollback();
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                }
            }
        }

        public HttpResponseMessage PostAnggota(penduduk value)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    value.Id = db.Penduduk.InsertAndGetLastID(value);

                    if (value.Detail != null )
                    {
                        value.Detail.Id = value.Id;
                       var isSaved= db.PendudukDetail.Insert(value.Detail);
                        var res = db.KKDetail.Insert(new kkdetail { KartuKeluargaId = value.KartuKeluarga.Id, PendudukId = value.Id });
                       if(value.Id<=0 || !isSaved || !res)
                        {
                            throw new SystemException("Data Gagal Ditambah");
                        }
                    }
                    else
                    {
                        throw new SystemException("Data Gagal Ditambah");
                    }

                    trans.Commit();
                    KartuKeluargaCollection collection = new KartuKeluargaCollection();

                    return Request.CreateResponse(HttpStatusCode.OK, collection.GetPendudukById(value.Id));
                }
                catch (Exception ex)
                {

                    trans.Rollback();
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                }
            }
        }

        // PUT: api/KartuKeluarga/5
        public HttpResponseMessage Put(int id,kartukeluarga value)
        {
            using (var db = new OcphDbContext())
            {
                if(id==value.Id)
                {
                    db.KartuKeluarga.Update(O => new { O.Alamat, O.KodePos, O.NoKK, O.RTId }, value, O => O.Id == id);

                    return Request.CreateResponse(HttpStatusCode.OK, "Data Berhasil Diubah");
                }else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Data Tidak Valid");
                }
            }



        }

        public HttpResponseMessage PutAnggota(penduduk penduduk)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    var isUpdatePenduduk = db.Penduduk.Update(O => new { O.Agama, O.JK, O.Nama, O.NIK, O.Pekerjaan, O.Pendidikan, O.TanggalLahir, O.TempatLahir }, penduduk, O => O.Id == penduduk.Id);
                    var isUpdateDetail = db.PendudukDetail.Update(O => new { O.Ayah, O.DokumenLain, O.HubunganDalamKeluarga, O.Ibu, O.Kewarganegaraan, O.Paspor, O.StatusPerkawinan }, penduduk.Detail, O => O.Id == penduduk.Id);
                    if (!isUpdateDetail || !isUpdatePenduduk)
                    {
                        throw new SystemException("Data Gagal Diubah");
                    }
                   
                    trans.Commit();
                    KartuKeluargaCollection collection = new KartuKeluargaCollection();

                    return Request.CreateResponse(HttpStatusCode.OK,penduduk);
                }
                catch (Exception ex)
                {

                    trans.Rollback();
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                }
            }
        }

        // DELETE: api/KartuKeluarga/5
        public HttpResponseMessage Delete(int id)
        {
            using (var db = new OcphDbContext())
            {
                if (db.KartuKeluarga.Delete( O => O.Id == id))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Data Berhasil Diubah");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Data Tidak Valid");
                }
            }


        }
    }
}
