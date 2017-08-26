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
                    item.DaftarRT = db.RT.Where(O => O.RWId == item.Id).ToList();
                    if(item.DaftarRT!=null)
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
                        if (value.Id > 0 && value.DaftarRT != null && value.DaftarRT.Count > 0)
                        {
                            foreach (var rt in value.DaftarRT)
                            {
                                if (rt.Id == 0)
                                {
                                    rt.RWId = value.Id;
                                    rt.Id = db.RT.InsertAndGetLastID(rt);
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

        // PUT: api/StrukturKelurahan/5
        public HttpResponseMessage Put(int id, rw value)
        {
            using(var db = new OcphDbContext())
            {
                var result = db.RW.Update(O => new { O.NoRW, O.PejabatId }, value, O => O.Id == id);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, value);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Data Tidak Dapat Diubah");
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
    }
}
