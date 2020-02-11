using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueAssistBot.Core.UserAccounts;
using Discord.WebSocket;
using System.Net;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.IO;
using LeagueAssistBot.Api;
using LeagueAssistBot.LeagueModel;

namespace LeagueAssistBot
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("whoisMaki")]
        public async Task Who()
        {
            await Context.Channel.SendMessageAsync("Flat is justice");
        }

        [Command("Server")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Server()
        {

        }

        [Command("ranking")]
        public async Task Ranking()
        {
            var Key = Config.bot.apiKey;
            string empty = "";
            string json = ""; string json2 = "";
            using (WebClient client = new WebClient())
            {
                json = client.DownloadString("https://eun1.api.riotgames.com/lol/summoner/v4/summoners/by-name/Kayn%20titties?api_key=RGAPI-07e96509-13a7-4fad-8859-2e85fd86e1d9");
                json2 = client.DownloadString("https://eun1.api.riotgames.com/lol/summoner/v4/summoners/by-name/Jinx%20titties?api_key=RGAPI-07e96509-13a7-4fad-8859-2e85fd86e1d9");
            }

            var dataObject = JsonConvert.DeserializeObject<dynamic>(json);

            string summonerName = dataObject.name.ToString();
            string summonerName2 = dataObject.name.ToString();
        //    string rank = dataObject.results[0].gender.ToString();

            var embed = new EmbedBuilder();
            embed.WithTitle("Ranking checked by " + Context.User.Username);
            embed.WithDescription($"1. [{summonerName}](https://eune.op.gg/summoner/userName=Kayn+titties)");
            embed.WithDescription($"1. [{summonerName2}](https://eune.op.gg/summoner/userName=Jinx+titties)");
            embed.AddField($"1. [{summonerName}](https://eune.op.gg/summoner/userName=Kayn+titties)", "Its mine now.", inline:true);
            embed.AddField($"2. [{summonerName2}](https://eune.op.gg/summoner/userName=Jinx+titties)", "Its mine now.", inline: true);
            embed.WithAuthor(summonerName, "", "https://eune.op.gg/summoner/userName=Kayn+titties");
            embed.WithAuthor(summonerName2, "", "https://eune.op.gg/summoner/userName=Jinx+titties");
            embed.WithUrl("https://eune.op.gg/summoner/userName=Kayn+titties");
            //     embed.AddField("Rank", rank);
            embed.WithColor(new Color(255, 255, 255));

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("Select")]
        public async Task Select()
        {
            string[] options = { "Normal", "Draft", "Aram", "Ranked", "Flex5v5", "Flex3v3", "TFTNormal", "TFTRanked" };

            Random r = new Random();
            string selection = options[r.Next(0, options.Length)];
            string command = "Select";

            var embed = new EmbedBuilder();
            embed.WithTitle("What game should u play?");
            embed.WithDescription(selection);
            embed.WithColor(new Color(255, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed.Build());

            DataStorage.AddPairToStorage(Context.User.Username + " | " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(), selection);
        }

        [Command("help")]
        public async Task HelpInfo([Remainder]string arg = "")
        {
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(Utilities.GetAlert("HELP"));
        }

        [Command("add")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Add([Remainder]string arg = "")
        {
            SocketUser target = null;
          //  var mentionedUser = 
        }

        [Command("del")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Del()
        {

        }

        [Command("level")]
        public async Task MyStats([Remainder]string arg = "")
        {
            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();

            target = mentionedUser ?? Context.User;

            var account = UserAccounts.GetAccount(target);
            await Context.Channel.SendMessageAsync($"{target.Username} has {account.Rank} Rank and {account.Points} experience");
        }

        [Command("addPoints")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task AddPoints(uint lp)
        {
            var account = UserAccounts.GetAccount(Context.User);
            account.Points += lp;
            UserAccounts.SaveAccounts();
            await Context.Channel.SendMessageAsync($"Points had been added successfully");
        }
    }
}
