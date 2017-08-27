using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using AspNet.Identity.MySQL;

namespace KelurahanSentani.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : MySQLDatabase
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public ApplicationDbContext(string connectionStringName) : base(connectionStringName)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext("DefaultConnection");
        }
    }

}