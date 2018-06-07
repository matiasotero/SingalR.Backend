using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match.Football.Service.Interface
{
    public interface IServiceHub
    {
        HubConnection ConfigurationService();
    }
}
