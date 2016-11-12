using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Legacy;
using Discord.Logging;

namespace Yuri_san
{
    class Program
    {
        static void Main(string[] args) => new Program().Start();

        public static DiscordClient Client;

        public void Start()
        {
            Client = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            Client.UsingCommands(e =>
            {
                e.PrefixChar = '`';
                e.HelpMode = HelpMode.Public;
                e.AllowMentionPrefix = true;
            });

            var botCommands = new BotCommands();

            Client.ExecuteAndWait(async () =>
            {
                await Client.Connect("MjQ2MDU5MDA2NDc5Njk1ODcy.CwVMQA.I5FDsiA7P9TZ4LHWpgxrRpD5VdU", TokenType.Bot);
                
            });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine($"[{e.Severity}] {e.Source}: {e.Message}");
        }
    }
}
