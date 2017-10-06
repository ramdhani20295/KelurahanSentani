using KelurahanSentani.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KelurahanSentani.Apis
{
    public class StrukturKelurahanController : ApiController
    {
        // GET: api/StrukturKelurahan
        public HttpResponseMessage GetRW()
        {
            using(var db = new OcphDbContext())
            {
                var result = db.RW.Select();
                foreach(var item in result)
                {
                    item.Pejabat = db.Pejabat.Where(O => O.Id == item.PejabatId).FirstOrDefault();
                    
                    var a = db.RT.Where(O => O.RWId == item.Id).ToList();
                    item.DaftarRT = a;
                    if (item.DaftarRT!=null)
                    {
                        foreach(var data in item.DaftarRT)
                        {
                            data.Pejabat = db.Pejabat.Where(O => O.Id == data.PejabatId).FirstOrDefault();
                        }
                    }

                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        // GET: api/StrukturKelurahan/5
        public HttpResponseMessage GetRw(int id)
        {
            using (var db = new OcphDbContext())
            {
                var item = db.RW.Where(O=>O.Id==id).FirstOrDefault();
                if(item!=null)
                {
                    item.DaftarRT = db.RT.Where(O => O.RWId == item.Id).ToList();
                    if (item.DaftarRT != null)
                    {
                        foreach (var data in item.DaftarRT)
                        {
                            data.Pejabat = db.Pejabat.Where(O => O.Id == data.PejabatId).FirstOrDefault();
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, item);
                }else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data Tidak Ditemukan");
                }
            }
        }

        // POST: api/StrukturKelurahan
        [HttpPost]
        public HttpResponseMessage PostRW(rw value)
        {
            using(var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    if (value != null && value.Id == 0)
                    {
                        value.Id = db.RW.InsertAndGetLastID(value);
                        db.Pejabat.Update(O => new { O.InstansiID }, new pejabat { InstansiID = value.Id }, O => O.Id == value.PejabatId);
                        if (value.Id > 0 && value.DaftarRT != null && value.DaftarRT.Count > 0)
                        {
                            foreach (var rt in value.DaftarRT)
                            {
                                if (rt.Id == 0)
                                {
                                    rt.RWId = value.Id;
                                    rt.Id = db.RT.InsertAndGetLastID(rt);
                                    db.Pejabat.Update(O => new { O.InstansiID }, new pejabat { InstansiID = rt.Id }, O => O.Id == rt.PejabatId);
                                }
                            }
                        }
                    }
                    else if(value != null && value.Id > 0)
                    {
                        foreach (var rt in value.DaftarRT)
                        {
                            if (rt.Id == 0)
                            {
                                rt.RWId = value.Id;
                                rt.Id = db.RT.InsertAndGetLastID(rt);
                                db.Pejabat.Update(O => new { O.InstansiID }, new pejabat { InstansiID = rt.Id }, O => O.Id == rt.PejabatId);

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

        [HttpPost]
        public HttpResponseMessage Postrt(rt value)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    value.Id = db.RT.InsertAndGetLastID(value);
                    if (value.Id > 0 && db.Pejabat.Update(O => new { O.InstansiID }, new pejabat { InstansiID = value.Id }, O => O.Id == value.PejabatId))
                    {
                        trans.Commit();
                        return Request.CreateResponse(HttpStatusCode.OK, value);
                    }
                    else
                        throw new SystemException("Data Gagal Ditambah");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                }
            }
        }

        // PUT: api/StrukturKelurahan/5
        public HttpResponseMessage Put(int id, rw value)
        {
            using(var db = new OcphDbContext())
            {
                var result = db.RW.Update(O => new { O.Nama, O.PejabatId }, value, O => O.Id == id);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, value);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Data Tidak Dapat Diubah");
            }
        }


        public HttpResponseMessage PutRT(int id, rt value)
        {
            using (var db = new OcphDbContext())
            {

                try
                {
                    if (value != null && value.Id == id)
                    {
                        var result = db.RT.Update(O => new { O.Nama, O.PejabatId}, value, O => O.Id == id);
                        if (result == true)
                            return Request.CreateResponse(HttpStatusCode.OK, value);
                        else
                            throw new SystemException("Data Gagal Diubah");
                    }
                    else
                        throw new SystemException("Data Gagal Diubah");

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                }
            }

        }


        // DELETE: api/StrukturKelurahan/5
        public HttpResponseMessage Delete(int id)
        {
            using(var db = new OcphDbContext())
            {
                var result = db.RW.Delete(O => O.Id == id);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, "Data Berhasil Dihapus");
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Data Tidak Dapat Dihapus");
            }
        }

        public HttpResponseMessage DeleteRT(int id)
        {
            using (var db = new OcphDbContext())
            {
                var result = db.RT.Delete(O => O.Id == id);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, "Data Berhasil Dihapus");
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Data Tidak Dapat Dihapus");
            }
        }
    }
}
