using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Authenticators.OAuth;

namespace Yuri_san
{
    class BotClient
    {
        public static DiscordClient Client;

        public BotClient()
        {
            Client = new DiscordClient(x =>
                {
                    x.LogLevel = LogSeverity.Info;
                    x.LogHandler = Log;
                }
            );

            Client.UsingCommands(x =>
            {
                x.PrefixChar = '`';
                x.AllowMentionPrefix = true;

            });
            var botCommands = new BotCommands();
            Client.ExecuteAndWait(async () =>
            {
                await Client.Connect("MjQ2MDU5MDA2NDc5Njk1ODcy.CwVMQA.I5FDsiA7P9TZ4LHWpgxrRpD5VdU", TokenType.Bot);
            });
        }

        private static void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
