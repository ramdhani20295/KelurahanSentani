﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public async Task GetTest()
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var result = controller.Get();
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod()]
        public void GetTestById()
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var result = controller.Get("12121");
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }


        [TestMethod()]
        public async Task PostTest()
        {
            var value = new penduduk { Agama = Kepercayaan.Islam, JK = Kelamin.Pria, Nama = "Chandra", NIK = "12121", Pekerjaan = "TNI", Pendidikan = Pendidikan.SMA, TanggalLahir = DateTime.Now, TempatLahir = "Palopo" };
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var result = controller.Post(value);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod()]
        public void PutTest()
        {
            var value = new penduduk { Id=15, Agama = Kepercayaan.Khatolik, JK = Kelamin.Pria, Nama = "Chandra", NIK = "12121", Pekerjaan = "TNI", Pendidikan = Pendidikan.SMA, TanggalLahir = DateTime.Now, TempatLahir = "Palopo" };
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var result = controller.Put(15, value);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var result = controller.Delete(15);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}