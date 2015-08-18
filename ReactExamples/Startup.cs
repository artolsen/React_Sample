using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReactExamples.Startup))]
namespace ReactExamples
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
