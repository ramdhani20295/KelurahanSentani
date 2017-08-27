using Microsoft.VisualStudio.TestTools.UnitTesting;
using KelurahanSentani.Apis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using System.Net;

namespace KelurahanSentani.Apis.Tests
{
    [TestClass()]
    public class KartuKeluargaControllerTests
    {
        KartuKeluargaController controller = new KartuKeluargaController();

        [TestMethod()]
        public void GetTest()
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var result = controller.Get();
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod()]
        public void GetTestByID()
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var result = controller.Get("12312");
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod()]
        public void PostTest()
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var result = controller.Post(new DataModels.kartukeluarga { Alamat="Jln", KodePos="1234", NoKK="123456", RTId=1 });
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod()]
        public void PutTest()
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var result = controller.Put(1,new DataModels.kartukeluarga {Id=1, Alamat = "Jln", KodePos = "1234", NoKK = "123456", RTId = 1 });
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var result = controller.Delete(1);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}