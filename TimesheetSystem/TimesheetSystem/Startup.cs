using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TimesheetSystem.Startup))]
namespace TimesheetSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }


    }
}
