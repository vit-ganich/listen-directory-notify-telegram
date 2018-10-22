using System.Linq;
using System.Threading.Tasks;
using TeleSharp.TL;
using TLSharp.Core;
using TeleSharp.TL.Messages;

namespace TestResultsReminder
{
    /// <summary>
    /// Class represents method for interactions with Telegram API via TLSharp library
    /// </summary>
    class TelegramExtension
    {
        public static TelegramClient NewClient()
        {
            return new TelegramClient(ConfigReader.GetApiId(), ConfigReader.GetApiHash());
        }

        /// <summary>
        /// When user is authenticated, TLSharp creates special file called session.dat.
        /// In this file TLSharp store all information needed for user session. 
        /// So you need to authenticate user every time the session.dat file is corrupted or removed.
        /// </summary>
        public static async Task AuthUserAsync()
        {
            var client = NewClient();
            await client.ConnectAsync();

            if (!client.IsUserAuthorized())
            {
               var hash = await client.SendCodeRequestAsync(ConfigReader.GetUserPhoneNumber());
                // authorization code will be send to the specified phone number
                var code = Helper.ReadTelegramCodeFromConsole();

                var user = await client.MakeAuthAsync(ConfigReader.GetUserPhoneNumber(), hash, code);
            }
        }

        public static async Task SendMessageAsync(string message)
        {
            if (ConfigReader.GetRecipientType() == "user")
            {
                await SendMessageToUserAsync(message);
            }
            else if(ConfigReader.GetRecipientType() == "channel")
            {
                await SendMessageToChannelAsync(message);
            }
        }

        public static async Task SendMessageToUserAsync(string message)
        {
            var client = NewClient();
            await client.ConnectAsync();

            // get available contacts
            var result = await client.GetContactsAsync();
            // find recipient in contacts
            TLUser user = result.Users.OfType<TLUser>().FirstOrDefault(x => x.Username == ConfigReader.GetRecipientName());
            // send message
            await client.SendMessageAsync(new TLInputPeerUser() { UserId = user.Id }, message);
        }

        public static async Task SendMessageToChannelAsync(string message)
        {
            var client = NewClient();
            await client.ConnectAsync();

            // get user dialogs
            var dialogs = (TLDialogs) await client.GetUserDialogsAsync();
            // find chat in contacts
            var chat = dialogs.Chats
                                    .Where(c => c.GetType() == typeof(TLChannel))
                                    .Cast<TLChannel>()
                                    .FirstOrDefault(c => c.Title == ConfigReader.GetRecipientName());
            // send message
            await client.SendMessageAsync(new TLInputPeerChannel() { ChannelId = chat.Id, AccessHash = chat.AccessHash.Value }, message);
        }
    }
}
