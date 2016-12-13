using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using Leaderboard.Models;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http.Cors;

namespace Leaderboard.Controllers
{
    [MobileAppController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomController : ApiController
    {
        // GET api/Custom
        public IHttpActionResult Get()
        {
            using (var db = new MobileServiceContext())
            {
                var list = db.Players.OrderByDescending(o => o.Score).Take(10).ToList();
                return Ok(list);
            }
            
        }
    }
}
