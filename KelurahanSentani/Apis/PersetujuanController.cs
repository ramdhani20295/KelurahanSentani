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
    public class PersetujuanController : ApiController
    {
        // GET: api/Persetujuan
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Persetujuan/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var a = from b in db.Persetujuan.Where(O => O.Id == id)
                            join c in db.Pejabat.Select() on b.IdPejabat equals c.Id
                            join d in db.Permohonan.Select() on b.PermohonanId equals d.Id
                            select new persetujuan { IdPejabat = b.IdPejabat, Id = b.Id, PermohonanId = b.PermohonanId, Pejabat = c, Permohonan = d, Setuju=b.Setuju };
                    return Request.CreateResponse(HttpStatusCode.OK, a.ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // POST: api/Persetujuan

       [Authorize]
        public HttpResponseMessage Post(permohonan data)
        {
            var pejabatCollection = new Collections.PejabatCollection();
            var uid = User.Identity.GetUserId();
            var pejabat = pejabatCollection.GetPejabatByUSerId(uid);
            try
            {
                if (pejabat == null)
                {
                   return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Anda Tidak Memiliki Hak Akses");
                }else
                {
                    var persetujuan = ApiHelper.CekPersetujuan(data.Id,pejabat.Level);
                    if(persetujuan.IsCompleted)
                    {
                        throw new SystemException("Persetujuan telah selesai");
                    }else
                    {
                       if(persetujuan.NotApproved.Contains(pejabat.Level))
                        {
                            using (var db = new OcphDbContext())
                            {
                                var item = new persetujuan { IdPejabat = pejabat.Id, PermohonanId = data.Id, Setuju=true };
                                item.Id = db.Persetujuan.InsertAndGetLastID(item);
                                if (item.Id > 0)
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, "Anda berhasil memberikan persetujuan");
                                }
                                else
                                    throw new SystemException("Persetujuan gagal diberikan");
                            }
                        }
                        else
                        {
                            throw new SystemException("Persetujuan telah diberikan");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [Authorize]
        [HttpPost]
        public HttpResponseMessage Unapproved(permohonan data)
        {
            var pejabatCollection = new Collections.PejabatCollection();
            var uid = User.Identity.GetUserId();
            var pejabat = pejabatCollection.GetPejabatByUSerId(uid);
            try
            {
                if (pejabat == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Anda Tidak Memiliki Hak Akses");
                }
                else
                {
                    var persetujuan = ApiHelper.CekPersetujuan(data.Id, pejabat.Level);
                    if (persetujuan.IsCompleted)
                    {
                        throw new SystemException("Persetujuan telah selesai");
                    }
                    else
                    {
                        if (persetujuan.NotApproved.Contains(pejabat.Level))
                        {
                            using (var db = new OcphDbContext())
                            {
                                var item = new persetujuan { IdPejabat = pejabat.Id, PermohonanId = data.Id, Setuju = false };
                                item.Id = db.Persetujuan.InsertAndGetLastID(item);
                                if (item.Id > 0)
                                {
                                    db.Permohonan.Update(O => new { O.Status }, data, O => O.Id == data.Id);
                                    return Request.CreateResponse(HttpStatusCode.OK, "Anda berhasil menolak");
                                }
                                else
                                    throw new SystemException("Persetujuan gagal diberikan");
                            }
                        }
                        else
                        {
                            throw new SystemException("Persetujuan telah diberikan");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        // PUT: api/Persetujuan/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Persetujuan/5
        public void Delete(int id)
        {
        }
    }
}
