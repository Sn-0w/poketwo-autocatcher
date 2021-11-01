using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Discord;
using Discord.Gateway;
using Microsoft.VisualBasic.FileIO;

namespace MessageLogger
{
    class Program
    {
        static List<string> pokermans = new List<string>();

        static void Main(string[] args)
        {
            Console.Title = "Snow's Poketwo Autocatcher | Sent No Messages | Caught No Pokemon";
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Starting Poketwo Autocatcher!");
            Console.WriteLine("Loading Database...");
            foreach (string line in System.IO.File.ReadLines(@"C:\Users\Snow\Desktop\Anarchy-dev\pokemon.csv"))
            {
                try
                {
                    string[] line_contents = line.Split(',');
                    try
                    {
                        Int32.Parse(line_contents[3]);
                    }
                    catch
                    {
                        pokermans.Add(line_contents[3]);
                    }
                }
                catch { }
            }
            Console.WriteLine("Loaded Database!");
            Console.WriteLine("Starting Bot!");
            DiscordSocketClient client = new DiscordSocketClient();
            client.OnLoggedIn += OnLoggedIn;
            client.OnMessageReceived += OnMessageReceived;

            client.Login("YOUR TOKEN");

            Thread.Sleep(-1);
        }

        public static string random(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var random = new Random();
            var randomString = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }



        private static void OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            Console.WriteLine("Started Bot!");
            Console.WriteLine($"Logged in as {args.User}");
            Console.WriteLine("Type !spam in a channel to start generating.");
            IMessageChannel test = (IMessageChannel)client.GetChannel(872788402746044446);
            int count = 0;
            while (true)
            {
                Console.Title = "Snow's Poketwo Autocatcher | Sent " + count + " Messages | Caught " + pokecatchcount + " Pokemon";
                Thread.Sleep(2000);
                count++;
                test.SendMessage(random() + "|" + count);
            }

        }

        static int pokecatchcount = 0;

        static void spammer(MinimalTextChannel targetchannel)
        {
            int count = 0;
            while (true)
            {
                Console.Title = "Snow's Poketwo Autocatcher | Sent " + count + " Messages | Caught " + pokecatchcount + " Pokemon";
                Thread.Sleep(2000);
                count++;
                targetchannel.SendMessage(random() + "|" + count);
            }
        }

        private static void OnMessageReceived(DiscordSocketClient client, MessageEventArgs args)
        {

            if (args.Message.Guild.Id == 841650214031851541 && args.Message.Embed != null)
            {
                if (args.Message.Embed.Title.Contains("wild pokémon has appeared!"))
                {
                    args.Message.Channel.SendMessage("!h");
                }
            }

            if (args.Message.Guild.Id == 841650214031851541 && args.Message.Embed == null)
            {
                if (args.Message.Content.Contains("The pokémon is"))
                {
                    string msg = args.Message.Content;
                    msg = msg.Replace("The pokémon is ", string.Empty);
                    msg = msg.Replace(".", string.Empty);
                    msg = msg.Replace(@"\", string.Empty);
                    msg = msg.Replace("_", "*");
                    Console.WriteLine("Located new Pokemon Drop!");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Hint is ==> "+ msg);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    foreach (string pokemon in pokermans)
                    {
                        if (pokemon.Length == msg.Length)
                        {
                            if (Regex.IsMatch(pokemon, makepattern(msg.ToLower())))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Pokemon is ==> "+ pokemon);
                                Console.ForegroundColor = ConsoleColor.Blue;
                                args.Message.Channel.SendMessage("!catch " + pokemon);
                                pokecatchcount++;
                            }
                        }
                    }
                }
            }


        }
        private static String makepattern(String value)
        {
            return "^" + Regex.Escape(value).Replace("\\*", ".*") + "$";
        }



        static void TypeWriteLineBreak(string message)
        {
            Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                Thread.Sleep(35);
            }
            Console.Write("\n");
        }

        static void TypeWrite(string message)
        {
            Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);
            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                Thread.Sleep(35);
            }
        }
    }
}
