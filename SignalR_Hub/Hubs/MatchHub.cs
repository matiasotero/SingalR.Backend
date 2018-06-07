using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalR_Hub.Model;

namespace SignalR_Hub.Hubs
{
    public class MatchHub: Hub
    {
        private static Queue<MatchPbP> _listMatchPbP;
        static MatchHub()
        {
            _listMatchPbP = new Queue<MatchPbP>();
        }

        public async Task SendPlayByPlay(MatchPbP matchPbP)
        {
            await Clients.Others.SendAsync("SendPlayByPlay", matchPbP);
            _listMatchPbP.Enqueue(matchPbP);
        }

        public async Task<MatchPbP> ReceivePlayByPlay()
        {
            await Clients.Others.SendAsync("ReceivePlayByPlay");
            return _listMatchPbP.Dequeue();
        }

        public async Task ClearQueue()
        {
            await Clients.Others.SendAsync("ClearQueue");
            _listMatchPbP.Clear();
        }
    }
}
