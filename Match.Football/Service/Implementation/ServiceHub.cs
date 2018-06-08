using Match.Football.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace Match.Football.Service.Implementation
{
    public class ServiceHub : IServiceHub
    {
        HubConnection _connection;
        IHubProxy _connectionProxy;

        public ServiceHub()
        {
            _connection = new HubConnection(ConfigurationManager.AppSettings["urlSignalR"], false);
            _connectionProxy = _connection.CreateHubProxy(ConfigurationManager.AppSettings["hubName"]);
        }

        public HubConnection Connection{ get => _connection; }
        public IHubProxy HubConnectionProxy { get => _connectionProxy; }
    }
}
