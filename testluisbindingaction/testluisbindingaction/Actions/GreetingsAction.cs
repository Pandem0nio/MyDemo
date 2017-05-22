using Microsoft.Cognitive.LUIS.ActionBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace testluisbindingaction
{
    [Serializable]
    [LuisActionBinding("Greetings", FriendlyName = "Greetings")]
    public class GreetingsAction : BaseLuisAction
    {
        public override Task<object> FulfillAsync()
        {
            return Task.FromResult((object)$"Ciao, come posso aiutarti?");
        }
    }
}