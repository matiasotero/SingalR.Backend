using Microsoft.AspNetCore.SignalR;
using SignalR_Backend_2.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arneb.API.LiveServices.Hubs
{
    public class myHub: Hub
    {
        private static Queue<MatchPbP> _listMatchPbP;
        static myHub()
        {
            _listMatchPbP = new Queue<MatchPbP>();
        }

        public async Task SendPlayByPlay(MatchPbP matchPbP)
        {
            await Clients.All.SendPlayByPlay(matchPbP);
            _listMatchPbP.Enqueue(matchPbP);
        }

        public async Task<MatchPbP> ReceivePlayByPlay()
        {
            await Clients.Others.ReceivePlayByPlay();
            return _listMatchPbP.Dequeue();
        }

        public async Task ClearQueue()
        {
            await Clients.Others.ClearQueue();
            _listMatchPbP.Clear();
        }
    }
}
