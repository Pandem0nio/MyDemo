

namespace testluisbindingaction
{
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Luis;
    using Microsoft.Bot.Connector;
    using Microsoft.Cognitive.LUIS.ActionBinding.Bot;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Reflection;
    using System.Threading.Tasks;

    [Serializable]
    internal class RootDialog : LuisActionDialog<object>
    {
        public RootDialog() : base(
            new Assembly[] { typeof(RootDialog).Assembly },
            new LuisService(new LuisModelAttribute(
                ConfigurationManager.AppSettings["LUIS_ModelId"], 
                ConfigurationManager.AppSettings["LUIS_SubscriptionKey"])))
        { }

        [LuisIntent("Greetings")]
        public async Task GreetingsHandlerAsync(IDialogContext context, object actionResult)
        {
            // we know these actions return a string for their related intents,
            // although you could have individual handlers for each intent
            var message = context.MakeMessage();

            message.Text = actionResult != null ? actionResult.ToString() : "Cannot resolve your query";

            await context.PostAsync(message);
        }

        [LuisIntent("SearchForEvents")]
        public async Task SearchForEventsHandlerAsync(IDialogContext context, object actionResult)
        {
            var message = context.MakeMessage();

            //Action logic
            if (actionResult != null)
            {
                if (actionResult.ToString().Contains("non ci sono eventi"))
                {
                    message.Text = actionResult.ToString();
                }
                else
                {
                    var eventlist = (List<Dictionary<string, string>>)actionResult;

                    //scorriamo gli eventi.... 
                    foreach (var e in eventlist)
                    {
                        CardAction regbutton = new CardAction()
                        {
                            Type = "postBack",
                            Title = "Registrami",
                            Value = "Registrami a " + e["eventname"]
                        };

                        CardAction infobutton = new CardAction()
                        {
                            Type = "postBack",
                            Title = "Informazioni",
                            Value = "Informazioni su " + e["eventname"]
                        };

                        List<CardAction> buttons = new List<CardAction>();
                        buttons.Add(regbutton);
                        buttons.Add(infobutton);

                        HeroCard plCard = new HeroCard()
                        {
                            Title = e["eventname"],
                            Subtitle = e["eventdate"],
                            Images = null,
                            Buttons = buttons
                        };

                        Attachment attachment = plCard.ToAttachment();

                        message.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                        message.Attachments.Add(attachment);

                    }
                }
            }

            await context.PostAsync(message);
        }

        [LuisIntent("RegisterToEvent")]
        public async Task RegisterToEventHandlerAsync(IDialogContext context, object actionResult)
        {
            var message = context.MakeMessage();

            message.Text = actionResult != null ? actionResult.ToString() : "Cannot resolve your query";

            await context.PostAsync(message);
        }
    }
}