using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Linq;
using Microsoft.Bot.Builder.Resource;

namespace BotDataMgmtTest
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            //ConnectorClient conn = new ConnectorClient(new Uri(activity.ServiceUrl));
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new Dialogs.EchoDialog());
                //int lenght = (activity.Text ?? string.Empty).Length;
                //Activity reply = activity.CreateReply($"Il messaggio inviato contiene {lenght} caratteri...");
                //await conn.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                var temp = HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private async Task HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                //Handle conversation state changes, like members being added and removed
                //Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                //Not available in all channels


               //Activity reply = message.CreateReply($"Benvenuto, come posso aiutarti?");
               // return reply;

                //there are two post message at the start: one to add the bot and one to add the user to the conversation
                //we send the welcome message only when the bot is added
                //if (message.MembersAdded.Any(o => o.Id == message.Recipient.Id))
                //{
                //    var reply = message.CreateReply($"Benvenuto, come posso aiutarti?");

                //    ConnectorClient connector = new ConnectorClient(new Uri(message.ServiceUrl));

                //    await connector.Conversations.ReplyToActivityAsync(reply);
                //}
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }
        }
    }
}