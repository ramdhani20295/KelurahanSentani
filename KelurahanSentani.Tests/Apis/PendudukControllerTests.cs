using Microsoft.VisualStudio.TestTools.UnitTesting;
using KelurahanSentani.Apis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KelurahanSentani.DataModels;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json;
using System.Threading;

namespace KelurahanSentani.Apis.Tests
{
    [TestClass()]
    public class PendudukControllerTests
    {
        PendudukController controller = new PendudukController();
        [TestMethod()]
        public async void GetTest()
        {
            var res = await controller.Get() as List<person>;
           
            Assert.AreEqual(res.Count, 0);
        }

        [TestMethod()]
        public void GetTestById()
        {
            var res = controller.Get(1);
            Assert.AreEqual(res,null);
        }


        [TestMethod()]
        public async Task PostTest()
        {
            var value = new person { Agama = "Islam", JK = "Laki", Nama = "Chandra", NIK="12121" };
            try
            {
                var result = controller.Post(value);
                Assert.AreEqual(HttpStatusCode.OK, result.IsSuccessStatusCode);
            }
            catch (Exception ex)
            {

                throw;
            }
           

          

        }
    }
}