using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Leaderboard.Startup))]

namespace Leaderboard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}