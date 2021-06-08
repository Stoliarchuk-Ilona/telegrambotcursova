using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.Types.ReplyMarkups;
using System.Text;
using System.Collections.Generic;

namespace Bot
{
    class Program
    {
        static ITelegramBotClient botClient;
        private static TelegramBotClient client;
        private static string Token { get; set; } = "1899833455:AAGHEVutcDt2Y7xdpFF4v9Dh9LI2jmWsAD4";
        static Uri GetUri(string path) => new Uri("https://localhost:44379" + path);
        static void Main()
        {
            botClient = new TelegramBotClient("1899833455:AAGHEVutcDt2Y7xdpFF4v9Dh9LI2jmWsAD4");
            client = new TelegramBotClient(Token);
            client.OnMessage += Bot_OnMessage;
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            client.StopReceiving();
            botClient.StopReceiving();
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            HttpClient client11 = new HttpClient();
            var msg = e.Message;

            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");

                if (e.Message.Text == "/getinfo")
                {
                    var Name = client.SendTextMessageAsync(msg.Chat.Id, "Enter the artist name to search for information:", replyMarkup: new ForceReplyMarkup() { Selective = true }).Result;
                }
                else if (e.Message.Text == "/getsimilar")
                {
                    var Name = client.SendTextMessageAsync(msg.Chat.Id, "Enter the artist name to search for similar ones:", replyMarkup: new ForceReplyMarkup() { Selective = true }).Result;
                }
                else if (e.Message.Text == "/gettopalbums")
                {
                    var Name = client.SendTextMessageAsync(msg.Chat.Id, "Enter the artist name to search for the top albums:", replyMarkup: new ForceReplyMarkup() { Selective = true }).Result;
                }
                else if (e.Message.Text == "/gettopsongs")
                {
                    var Name = client.SendTextMessageAsync(msg.Chat.Id, "Enter the artist name to search for the top songs:", replyMarkup: new ForceReplyMarkup() { Selective = true }).Result;
                }
                else if (e.Message.Text == "/addtofavorites")
                {
                    var Name = client.SendTextMessageAsync(msg.Chat.Id, "Enter the artist name to add to favorites:", replyMarkup: new ForceReplyMarkup() { Selective = true }).Result;
                }
                else if (e.Message.Text == "/deletefromfavorites")
                {
                    var Name = client.SendTextMessageAsync(msg.Chat.Id, "Enter the artist name to delete from favorites:", replyMarkup: new ForceReplyMarkup() { Selective = true }).Result;
                }
                /*else if (e.Message.Text == "/getallfavorites")
                {

                    var response = await client11.GetAsync(GetUri($"/api/Artist/all_favorites"));
                    response.EnsureSuccessStatusCode();
                    var content = response.Content.ReadAsStringAsync().Result;
                    var artist = JsonConvert.DeserializeObject<ArtistResponse5>(content);

                    var result = new ArtistResponse5
                    {
                        Favorites = artist.Favorites,
                    };

                    for (int i = 1; i < result.Favorites.Length; i++)
                    {
                        await botClient.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: $"{i}. " + result.Favorites[i].Artist + "\n"
                    );
                    }

                }*/

            }


            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter the artist name to search for the top songs:"))
            {
                var response = await client11.GetAsync(GetUri($"/api/Artist/tracks?NameOfArtist={e.Message.Text}"));
                response.EnsureSuccessStatusCode();
                var content = response.Content.ReadAsStringAsync().Result;

                var artist = JsonConvert.DeserializeObject<ArtistResponse3>(content);

                var result = new ArtistResponse3
                {
                    Tracks = artist.Tracks,
                };
                await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: $"Top songs of {e.Message.Text}:\n" + "1. " + result.Tracks[0].Name + "\n" + "2. " + result.Tracks[1].Name + "\n" + "3. " + result.Tracks[2].Name
                );
            }
            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter the artist name to search for the top albums:"))
            {
                var response = await client11.GetAsync(GetUri($"/api/Artist/albums?NameOfArtist={e.Message.Text}"));
                response.EnsureSuccessStatusCode();
                var content = response.Content.ReadAsStringAsync().Result;

                var artist = JsonConvert.DeserializeObject<ArtistResponse2>(content);

                var result = new ArtistResponse2
                {
                    Albums = artist.Albums,
                };
                await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: $"Top albums of {e.Message.Text}:\n" + "1. " + result.Albums[0].Name + "\n" + "2. " + result.Albums[1].Name + "\n" + "3. " + result.Albums[2].Name
                );
            }
            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter the artist name to search for similar ones:"))
            {
                var response = await client11.GetAsync(GetUri($"/api/Artist/similar?NameOfArtist={e.Message.Text}"));
                response.EnsureSuccessStatusCode();
                var content = response.Content.ReadAsStringAsync().Result;

                var artist = JsonConvert.DeserializeObject<ArtistResponse4>(content);

                var result = new ArtistResponse4
                {
                    Similar = artist.Similar,
                };
                await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: $"Artists similar to {e.Message.Text}:\n" + "1. " + result.Similar[0].Name + "\n" + "2. " + result.Similar[1].Name + "\n" + "3. " + result.Similar[2].Name
                );
            }
            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter the artist name to search for information:"))
            {
                var response = await client11.GetAsync(GetUri($"/api/Artist/inf?NameOfArtist={e.Message.Text}"));
                response.EnsureSuccessStatusCode();
                var content = response.Content.ReadAsStringAsync().Result;
                var artist = JsonConvert.DeserializeObject<ArtistResponse>(content);

                var result = new ArtistResponse
                {
                    Artist = artist.Artist,
                    Description = artist.Description,
                };
                await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: $"Information about {e.Message.Text}:\n" + result.Description
                );
            }
            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter the artist name to add to favorites:"))
            {
                var response = await client11.GetAsync(GetUri($"/api/Artist/inf?NameOfArtist={e.Message.Text}"));
                response.EnsureSuccessStatusCode();
                var content = response.Content.ReadAsStringAsync().Result;
                var artist = JsonConvert.DeserializeObject<ArtistResponse>(content);

                var json = JsonConvert.SerializeObject(artist);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var post = await client11.PostAsync(GetUri($"/api/Artist/add"), data);
                await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: $"Information about {e.Message.Text} added to favorites"
                );
            }
            if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Enter the artist name to delete from favorites:"))
            {
                var delete = await client11.DeleteAsync(GetUri($"/api/Artist/delete?artist={e.Message.Text}"));
                await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: $"Information about {e.Message.Text} deleted from favorites"
                );
            }

        }
    }
}