using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using HtmlAgilityPack;

namespace Yuri_san
{
    class BotCommands : CommandService
    {
        public CommandService Commands { get; set; }

        public BotCommands()
        {
            Commands = Program.Client.GetService<CommandService>();
            Commands.CreateGroup("", cmd =>
            {
                cmd.CreateCommand("loli")
                    .Description("Posts a loli")
                    .Do((async e =>
                    {
                        await e.Channel.SendMessage(ImageSearch("loli"));                       
                    }));
                cmd.CreateCommand("lolihug")
                    .Description("Loli hugging")
                    .Do((async e =>
                    {
                        await e.Channel.SendMessage(ImageSearch("loli hugging"));

                    }));
                cmd.CreateCommand("search")
                    .Description("Custom image search")
                    .Parameter("subject", ParameterType.Unparsed)
                    .Do((async e =>
                    {
                        await e.Channel.SendMessage(CustomSearch(e.GetArg("subject")));
                    }));
            });
        }

        ///public void ImageSearch(string subject, string fileName)
        public string ImageSearch(string subject)
        {
            var webget = new HtmlWeb();
            var source = webget.Load("http://safebooru.org/index.php?page=post&s=list&tags=loli");
            var randomIndex = new Random();
            List<string> imgArrays = new List<string>();
            HtmlNode[] nodes = source.DocumentNode.SelectNodes("//img").ToArray();
            foreach (var node in nodes)
            {
               imgArrays.Add(node.Attributes["src"].Value);
            }
            return imgArrays[randomIndex.Next(imgArrays.Count)];
        }

        public string CustomSearch(string subject)
        {
            const string apiKey = "AIzaSyCIVBqRBZQcH4 - yqkyshzhJC37v4CfJgtE";
            const string searchEngineId = "009204377345296057423:2iq7quuu3xq";
            string query = subject;
            var searchIndex = new Random();

            var customsearchService = new CustomsearchService(new BaseClientService.Initializer() { ApiKey = apiKey });
            var listRequest = customsearchService.Cse.List(query);

            listRequest.Cx = searchEngineId;
            listRequest.SearchType = CseResource.ListRequest.SearchTypeEnum.Image;
            Search search = listRequest.Execute();
            

            foreach (var item in search.Items)
            {
                Console.WriteLine(item.Title);
            }
            return search.Items[searchIndex.Next(search.Items.Count)].Link.Equals(null) ? "No images found. Sorry." : search.Items[searchIndex.Next(search.Items.Count)].Link;
        }
    }
}
