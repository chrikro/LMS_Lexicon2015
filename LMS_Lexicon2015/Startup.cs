using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LMS_Lexicon2015.Startup))]
namespace LMS_Lexicon2015
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
