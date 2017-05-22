using Microsoft.Cognitive.LUIS.ActionBinding;
using Microsoft.IdentityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace testluisbindingaction
{
    [Serializable]
    [LuisActionBinding("SearchForEvents", FriendlyName = "Search Event")]
    public class SearchForEventsAction : BaseLuisAction
    {
        [Required(ErrorMessage = "Inserisci una città")]
        [LuisActionBindingParam(CustomType = "Place")]
        public string Place { get; set; }
        [Required(ErrorMessage = "Inserisci un topic")]
        [LuisActionBindingParam(CustomType = "Technology")]
        public string Technology { get; set; }

        public override Task<object> FulfillAsync()
        {
            //TODOO: only for the demo....
            if (this.Technology.ToLower().Contains("azure")){
                List<Dictionary<string, string>> eventlist = new List<Dictionary<string, string>>();
                Dictionary<string, string> event1 = new Dictionary<string, string>();
                event1.Add("eventname", "Global Azure Bootcamp Pordenone");
                event1.Add("eventdate", "22/04/2017");
                event1.Add("eventtime", "09:00 - 18:00");
                event1.Add("description","blablablabla");
                eventlist.Add(event1);
                Dictionary<string, string> event2 = new Dictionary<string, string>();
                event2.Add("eventname", "BizSpark Camp Pordenone");
                event2.Add("eventdate", "26/04/2017");
                event2.Add("eventtime", "09:00 - 18:00");
                event2.Add("description", "blablablabla");
                eventlist.Add(event2);
                return Task.FromResult((object)eventlist);

            }
            else
            {
                return Task.FromResult((object)$"Mi spiace, non ci sono eventi {this.Technology} a {this.Place} per il momento, ma puoi provare con una nuova ricerca.");
            }

        }
    }
}