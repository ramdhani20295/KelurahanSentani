using KelurahanSentani.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace KelurahanSentani.Apis
{
    public class PendudukController : ApiController
    {
      
        // GET: api/Penduduk
        public async Task<IEnumerable<person>> Get()
        {
            using (var db = new OcphDbContext())
            {
                var result= db.Penduduk.Select();
                return await Task.FromResult(result);

            }
        }

        // GET: api/Penduduk/5
        public person Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                return db.Penduduk.Where(o => o.Id == id).FirstOrDefault();

            }
        }

        // POST: api/Penduduk
        [HttpPost]
        public  HttpResponseMessage Post(person value)
        {
            using(var db = new OcphDbContext())
            {
                try
                {
                    return Request.CreateResponse(HttpStatusCode.OK, db.Penduduk.InsertAndGetLastID(value));
                }
                catch (Exception ex)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                }
            
            }
        }

        // PUT: api/Penduduk/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Penduduk/5
        public void Delete(int id)
        {
        }
    }
}
