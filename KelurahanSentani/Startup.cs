using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KelurahanSentani.Startup))]
namespace KelurahanSentani
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
