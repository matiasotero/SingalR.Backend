using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match.Football.Service.Interface
{
    public interface IServiceHub
    {
        HubConnection Connection { get; }
        IHubProxy HubConnectionProxy { get; }
    }
}
