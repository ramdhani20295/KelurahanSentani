using KelurahanSentani.DataModels;
using KelurahanSentani.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace KelurahanSentani.Apis
{
    public class AccountController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        // GET: api/Account
        // GET: api/Account


        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public AccountController()
        {
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Account/5
        [AllowAnonymous]

        [HttpGet]
        public async Task<HttpResponseMessage> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error");
            }

            var c = HttpUtility.UrlDecode(code);
            var result = await UserManager.ConfirmEmailAsync(userId, c);
            return Request.CreateResponse(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        // POST: api/Account
        public async Task<HttpResponseMessage> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var data = JsonConvert.SerializeObject(model);
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var role = await UserManager.AddToRoleAsync(user.Id, "Pejabat");
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string c = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    string code = HttpUtility.UrlEncode(c);
                    var callbackUrl = Url.Link("DefaultApi", new { controller = "Account/ConfirmEmail", userId = user.Id, code = code });
                    await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    using (var db = new OcphDbContext())
                    {
                        model.usersId = user.Id;
                      model.Id=  db.Pejabat.InsertAndGetLastID(model);
                    }
                }
                return Request.CreateResponse(model);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error");
            }

            // If we got this far, something failed, redisplay form
        }

        // PUT: api/Account/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Account/5
        public void Delete(int id)
        {
        }
    }
}
