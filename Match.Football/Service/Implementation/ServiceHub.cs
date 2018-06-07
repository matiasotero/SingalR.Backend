using Match.Football.Service.Interface;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Match.Football.Service.Implementation
{
    public class ServiceHub : IServiceHub
    {
        HubConnection _connection;
        public HubConnection ConfigurationService()
        {
            var url = ConfigurationManager.AppSettings["urlSignalR"];
            _connection = new HubConnectionBuilder()
               .WithUrl(url)
               .Build();
            return _connection;
        }
    }
}
