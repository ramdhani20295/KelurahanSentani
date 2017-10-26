using KelurahanSentani.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using System.Linq;

namespace KelurahanSentani.Apis
{
    public class AccountController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _appRoleManager;

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

        [Authorize]
        [HttpGet]
        public HttpResponseMessage MyRoles()
        {
            var uid = User.Identity.GetUserId();
            var role = UserManager.GetRoles(uid).FirstOrDefault();
           return Request.CreateResponse(HttpStatusCode.OK, role);
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

        public ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _appRoleManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _appRoleManager = value;
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
                try
                {
                    var data = JsonConvert.SerializeObject(model);
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    string role = model.Level.ToString();
                    if (result.Succeeded)
                    {
                        var isExis = await AppRoleManager.RoleExistsAsync(role);
                        if (!isExis)
                        {
                            var r = await AppRoleManager.CreateAsync(new AspNet.Identity.MySQL.IdentityRole { Name = role });
                            if (r.Succeeded)
                            {
                                var roleResult = await UserManager.AddToRoleAsync(user.Id, role);
                            }else
                            {
                                throw new System.Exception( string.Format("Role {0} Gagal Dibuat, Hubungi Administrator", role));
                            }
                        }
                        else
                        {
                            var roleResult = await UserManager.AddToRoleAsync(user.Id, role);
                            if(!roleResult.Succeeded)
                            {
                                throw new System.Exception(string.Format("Gagal Menambahkan User Role"));
                            }
                        }
                        // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        //string code = HttpUtility.UrlEncode(c);
                        var callbackUrl = Url.Link("Default", new { controller = "Account/ConfirmEmail", userId = user.Id, code = code });
                        await UserManager.SendEmailAsync(user.Id, "Confirm your account", "<p>Konfimasi Akun</p><p> Akun Anda Telah Dibuat:</p><p> User Name: "+ user.Email +"</p><p> Password : "+ model.Password+" </p> "+ "Silahkan confirm  account anda dengan mengklick link <a href=\"" + callbackUrl + "\">disini</a>");
                        using (var db = new OcphDbContext())
                        {
                            model.usersId = user.Id;
                            model.Id = db.Pejabat.InsertAndGetLastID(model);
                        }
                    }
                    return Request.CreateResponse(model);
                }
                catch (System.Exception ex)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error");
                }
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
