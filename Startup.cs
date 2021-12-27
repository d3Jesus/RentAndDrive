using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RentAndDrive.Startup))]
namespace RentAndDrive
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
