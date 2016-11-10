using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Google.Apis.Customsearch;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;

namespace Yuri_san
{
    class BotCommands : CommandService
    {
        public CommandService Commands { get; set; }
        public BotCommands()
        {
            Commands = BotClient.Client.GetService<CommandService>();
            Commands.CreateGroup("", cmd =>
            {
                cmd.CreateCommand("loli")
                    .Description("Posts a loli")
                    .Do((async e =>
                    {
                        ImageSearch("loli", "tmp.png");
                        await e.Channel.SendFile(@".\img\tmp.png");

                    }));
                cmd.CreateCommand("lolihug")
                    .Description("Loli hugging")
                    .Do((async e =>
                    {
                        ImageSearch("loli hug", "tmp1.png");
                        await e.Channel.SendFile(@".\img\tmp1.png");

                    }));
            });
        }

        public void ImageSearch(string subject, string fileName)
        {
            const string apiKey = "AIzaSyCIVBqRBZQcH4 - yqkyshzhJC37v4CfJgtE";
            const string searchEngineId = "009204377345296057423:2iq7quuu3xq";
            string query = subject;

            var customsearchService = new CustomsearchService(new BaseClientService.Initializer() {ApiKey = apiKey});
            var listRequest = customsearchService.Cse.List(query);

            listRequest.Cx = searchEngineId;
            listRequest.SearchType = CseResource.ListRequest.SearchTypeEnum.Image;
            Search search = listRequest.Execute();
            Random searchIndex = new Random();
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile(new Uri(search.Items[searchIndex.Next(search.Items.Count)].Link),
                    @".\img\" + fileName);
            }
        }
    }
}
