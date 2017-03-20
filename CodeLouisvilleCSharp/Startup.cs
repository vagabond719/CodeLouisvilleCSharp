using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodeLouisvilleCSharp.Startup))]
namespace CodeLouisvilleCSharp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
