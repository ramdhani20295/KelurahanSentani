﻿using KelurahanSentani.DataModels;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Surat/5
        public string Get(int id)
        {
            return "value";
        }

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

        // PUT: api/Surat/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Surat/5
        public void Delete(int id)
        {
        }
    }
}