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
using KelurahanSentani.DataModels;

namespace KelurahanSentani.Apis.Tests
{
    [TestClass()]
    public class StrukturKelurahanControllerTests
    {
        StrukturKelurahanController controller = new StrukturKelurahanController();
        [TestMethod()]
        public void GetRWTest()
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var result = controller.GetRW();
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod()]
        public void GetRwByRwIdTest()
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var result = controller.GetRw(1);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod()]
        public void PostRWTest()
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            List<rt> listrt = new List<rt>();
            listrt.Add(new rt { Nama = "1" , PejabatId=3});
          

            var result = controller.PostRW(new DataModels.rw { Nama="1", Id=0,DaftarRT=listrt, PejabatId=4  });
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }


        [TestMethod()]
        public void PostRTWhenRWthereTest()
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            List<rt> listrt = new List<rt>();
            listrt.Add(new rt { Nama = "1", PejabatId = 5 , RWId=4});
            var result = controller.PostRW(new DataModels.rw { Nama = "1", Id =4, DaftarRT = listrt, PejabatId = 4  });
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }


        [TestMethod()]
        public void PutTest()
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var result = controller.Put(1, new DataModels.rw { Id=1, Nama = "1", PejabatId = 1 });
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