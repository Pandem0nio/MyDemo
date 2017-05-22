using Microsoft.Cognitive.LUIS.ActionBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace testluisbindingaction
{
    [Serializable]
    [LuisActionBinding("RegisterToEvent", FriendlyName = "Register to an event")]
    public class RegisterToEventAction : BaseLuisAction
    {
        [Required(ErrorMessage = "Inserisci il tuo nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Inserisci la tua mail")]
        //[Mail(ErrorMessage = "Inserisci una mail valida")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Inserisci il tuo numero")]
        //[Phone(ErrorMessage = "Inserisci un numero valido")]
        public string Number { get; set; }

        public override Task<object> FulfillAsync()
        {
            return Task.FromResult((object)$"Grazie {this.Name} per esserti iscritto. Riceverai una mail di conferma all'indirizzo {this.Mail}.");
        }
    }
}