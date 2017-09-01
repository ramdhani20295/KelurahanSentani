using KelurahanSentani.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KelurahanSentani.Apis
{
    public class PejabatController : ApiController
    {
        // GET: api/Pejabat
        // GET: api/Penduduk

            [Authorize(Roles ="Administrator")]
        public HttpResponseMessage Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Pejabat.Select();
                foreach (var item in result)
                {
                    if(item.Level== LevelStruktur.RW)
                    {
                        item.Instansi = db.RW.Where(O => O.Id == item.InstansiID).FirstOrDefault();
                    }else if(item.Level== LevelStruktur.RT)
                    {
                        item.Instansi = db.RT.Where(O => O.Id == item.InstansiID).FirstOrDefault();
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, result.ToList());
            }
        }

        // GET: api/Penduduk/5
        public HttpResponseMessage Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                var item = db.Pejabat.Where(O => O.Id==id).FirstOrDefault();
                if (item!=null &&item.Level == LevelStruktur.RW)
                {
                    item.Instansi = db.RW.Where(O => O.Id == item.InstansiID).FirstOrDefault();
                }
                else if (item != null &&item.Level == LevelStruktur.RT)
                {
                    item.Instansi = db.RT.Where(O => O.Id == item.InstansiID).FirstOrDefault();
                }
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
        }

        // POST: api/Penduduk
        [HttpPost]
        public HttpResponseMessage Post(pejabat value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.Pejabat.InsertAndGetLastID(value);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                }
            }
        }

        // PUT: api/Penduduk/5
        public HttpResponseMessage Put(int id, pejabat value)
        {
            using (var db = new OcphDbContext())
            {

                try
                {
                    if (value != null && value.Id == id)
                    {
                        var result = db.Pejabat.Update(O => new { O.Alamat,O.InstansiID,O.Jabatan,O.Level,O.Nama,O.Status}, value, O => O.Id == id);
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

        // DELETE: api/Penduduk/5
        public HttpResponseMessage Delete(int id)
        {
            using (var db = new OcphDbContext())
            {

                try
                {
                    if (id > 0)
                    {
                        var result = db.Pejabat.Delete(O => O.Id == id);
                        if (result == true)
                            return Request.CreateResponse(HttpStatusCode.OK, result);
                        else
                            throw new SystemException("Data Gagal Diubah");
                    }
                    else
                        throw new SystemException("Data Gagal Dihapus");

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                }
            }
        }
    }
}
