using Microsoft.VisualStudio.TestTools.UnitTesting;
using KelurahanSentani.Apis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KelurahanSentani.Models;
using System.Net.Http;
using System.Web.Http;
using System.Net;

namespace KelurahanSentani.Apis.Tests
{
    [TestClass()]
    public class AccountControllerTests
    {
        Apis.AccountController controller = new Apis.AccountController();
        [TestMethod()]
        public async Task RegisterTestAsync()
        {
            var value = new RegisterViewModel { Alamat = "", ConfirmPassword = "Pejabat#1", Password = "Pejabat#1", Email = "kristt26@gmail.com", InstansiID = 0, Jabatan = JenisJabatan.Ketua, Level = LevelStruktur.RT, Nama = "Ajenk", Status = true };

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var result = await controller.Register(value);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        }
    }
}