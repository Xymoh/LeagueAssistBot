using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueAssistBot.Core.RankSystem
{
    internal static class Ranking
    {
        internal static async void UserSentMessage(SocketGuildUser user, SocketTextChannel channel)
        {
            // if the user has a timeout, ignore them

            var userAccount = UserAccounts.UserAccounts.GetAccount(user);
            uint oldRank = userAccount.Rank;
            userAccount.Points += 50;
            UserAccounts.UserAccounts.SaveAccounts();
            uint newRank = userAccount.Rank;

            if(oldRank != newRank)
            {
                await channel.SendMessageAsync(user.Username + " just ranked up to " + newRank + "!");
            }

        }
    }
}
