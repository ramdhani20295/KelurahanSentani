﻿using KelurahanSentani.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DAL.Extentions;

namespace KelurahanSentani.Apis
{
    public class PendudukController : ApiController
    {
      
        // GET: api/Penduduk
        public HttpResponseMessage Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Penduduk.Select();
                foreach(var item in result)
                {
                    var kkdetail = db.KKDetail.Where(O => O.PendudukId == item.Id).FirstOrDefault();
                    if(kkdetail!=null)
                        item.KartuKeluarga = db.KartuKeluarga.Where(O => O.Id == kkdetail.KartuKeluargaId).FirstOrDefault();
                }
                return Request.CreateResponse(HttpStatusCode.OK, result.ToList());
            }
        }

        // GET: api/Penduduk/5
        public HttpResponseMessage Get(string NIK)
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Penduduk.Where(O=>O.NIK==NIK).FirstOrDefault();
               if(result!=null)
                {
                    var kkdetail = db.KKDetail.Where(O => O.PendudukId == result.Id).FirstOrDefault();
                    if (kkdetail != null)
                        result.KartuKeluarga = db.KartuKeluarga.Where(O => O.Id == kkdetail.KartuKeluargaId).FirstOrDefault();
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        // POST: api/Penduduk
        [HttpPost]
        public  HttpResponseMessage Post(penduduk value)
        {
            using(var db = new OcphDbContext())
            {
                try
                {
                    var result = db.Penduduk.InsertAndGetLastID(value);
                    return Request.CreateResponse(HttpStatusCode.OK,result);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                }
            }
        }

        // PUT: api/Penduduk/5
        public HttpResponseMessage Put(int id , penduduk value)
        {
            using (var db = new OcphDbContext())
            {
             
                try
                {
                    if (value != null && value.Id == id)
                    {
                        var result = db.Penduduk.Update(O => new { O.Agama, O.JK, O.Nama, O.NIK, O.Pekerjaan, O.Pendidikan, O.TanggalLahir, O.TempatLahir }, value, O => O.Id == id);
                        if(result ==true)
                            return Request.CreateResponse(HttpStatusCode.OK, result);
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
                    if (id>0)
                    {
                        var result = db.Penduduk.Delete(O => O.Id==id);
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
