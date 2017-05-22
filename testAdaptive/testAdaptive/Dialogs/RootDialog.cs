using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using AdaptiveCards;
using System.Collections.Generic;

namespace testAdaptive.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            IMessageActivity replyToConversation = context.MakeMessage();
            var activity = await result as Activity;
            if(activity.Text != null)
            {
                string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/bin/card.json");
                
                var card = JsonConvert.DeserializeObject<AdaptiveCard>(json);

                // Create the attachment.
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };
                replyToConversation.Attachments.Add(attachment);
                await context.SayAsync(activity.Text, activity.Text);
                await context.PostAsync(replyToConversation);
            }
            else if (activity.Value != null)
            {
                var dato = JsonConvert.DeserializeObject<Dictionary<string,string>>(activity.Value.ToString());
                string[] listdato = dato["test"].Split(';');

                string testo = "";

                foreach(string s in listdato)
                {
                    testo += " " + s + " - ";
                }
                
                AdaptiveCard card = new AdaptiveCard();

                card.Body.Add(new Image()
                {
                    Url = "https://upload.wikimedia.org/wikipedia/commons/3/38/Robot-clip-art-book-covers-feJCV3-clipart.png"
                });

                card.Body.Add(new TextBlock()
                {
                    Text = "Selected item: ",
                    Size = TextSize.Large,
                    Weight = TextWeight.Bolder,
                    Color = TextColor.Attention
                });

                // Add text to the card.
                card.Body.Add(new TextBlock()
                {
                    Text = testo,
                    Size = TextSize.Large,
                    Weight = TextWeight.Normal
                });

                // Create the attachment.
                Attachment attachment = new Attachment()
                {
                    ContentType = AdaptiveCard.ContentType,
                    Content = card
                };
                replyToConversation.Attachments.Add(attachment);
                await context.PostAsync(replyToConversation);
            }
            else
            {
                await context.PostAsync("altro...");
            }
            
            context.Wait(MessageReceivedAsync);
        }
    }
}