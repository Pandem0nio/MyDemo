using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace BotDataMgmtTest.Dialogs
{
    [Serializable]
    public class EchoDialog : IDialog<object>
    {

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            string userName;
            bool welcomed;
            
            if (!context.UserData.TryGetValue("UserName", out userName))
            {
                
                PromptDialog.Text(context, this.ResumeAfterPrompt, "Ciao :) Prima di incominciare, qual è il tuo nome?");
                return;
            }

            welcomed = context.ConversationData.TryGetValue<bool>("Welcomed", out welcomed);
            if (!welcomed)
            {
                //this.userWelcomed = true;
                context.ConversationData.SetValue<bool>("Welcomed", true);
                PromptDialog.Confirm(context, this.ResumeAfterConfirm, $"Benvenuta {context.UserData.Get<string>("UserName")} Vuoi cambiare il tuo nome?");
                return;
            }

            var message = await result;
            await context.PostAsync("Hai detto: " + message.Text);
            context.Wait(MessageReceivedAsync);
        }

        private async Task ResumeAfterPrompt(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                var userName = await result;
                context.ConversationData.SetValue<bool>("Welcomed", true);

                await context.PostAsync($"Benvenuto/a {userName}!");

                context.UserData.SetValue("UserName", userName);
            }
            catch (TooManyAttemptsException)
            {
            }

            context.Wait(this.MessageReceivedAsync);
        }

        private async Task ResumeAfterConfirm(IDialogContext context, IAwaitable<bool> result)
        {
            try
            {
                var confirm = await result;
                if (confirm)
                {

                    PromptDialog.Text(context, this.ResumeAfterChange, "Qual è il tuo nome?");
                    return;
                }
                else
                {
                    await context.PostAsync($"Ok, {context.UserData.Get<string>("UserName")}");
                }
            }
            catch (TooManyAttemptsException)
            {
            }

            context.Wait(this.MessageReceivedAsync);
        }

        private async Task ResumeAfterChange(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                var userName = await result;
                context.ConversationData.SetValue<bool>("Welcomed", true);

                await context.PostAsync($"Ho modificato il nome in {userName}!");

                context.UserData.SetValue("UserName", userName);
            }
            catch (TooManyAttemptsException)
            {
            }

            context.Wait(this.MessageReceivedAsync);
        }
    }

}