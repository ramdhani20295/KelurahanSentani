using KelurahanSentani.DataModels;
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
        public IEnumerable<kartukeluarga> Get()
        {
            using (var db = new OcphDbContext())
            {
                var listKK = db.KartuKeluarga.Select();
                foreach(var item  in listKK)
                {
                    List<penduduk> peduduks = new List<penduduk>();
                  foreach(var data in db.KKDetail.Where(O=>O.KartuKeluargaId==item.Id))
                        {
                        var p = db.Penduduk.Where(O => O.Id == data.PendudukId).FirstOrDefault();
                        if (p != null)
                            peduduks.Add(p);
                    }
                    item.DaftarKeluarga = peduduks;
                }

                return listKK;
            }
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
                    return Request.CreateResponse(HttpStatusCode.OK, kk);
                }else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data Tidak Ditemukan");
                }
            }
        }

        // POST: api/KartuKeluarga
        public HttpResponseMessage Post(kartukeluarga value)
        {
            using(var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    value.Id = db.KartuKeluarga.InsertAndGetLastID(value);
                    if(value.DaftarKeluarga!=null && value.DaftarKeluarga.Count>0)
                    {
                        foreach(var item in value.DaftarKeluarga)
                        {
                            if(item.Id==0)
                            {
                                item.Id = db.Penduduk.InsertAndGetLastID(item);
                                db.KKDetail.Insert(new kkdetail { KartuKeluargaId = value.Id, PendudukId = item.Id });
                            }
                        }
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
