using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leaderboard.DataObjects
{
    public class Player : EntityData
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }
}