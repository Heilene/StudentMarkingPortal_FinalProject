using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VisionCollegeDSEDMarkingPortal.Startup))]
namespace VisionCollegeDSEDMarkingPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
