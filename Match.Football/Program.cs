using Match.Football.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Match.Football.Service.Implementation;

namespace Match.Football
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHub _serviceHub = new ServiceHub();

            Console.WriteLine("Comienzo de servicio de partido en vivo!!");
            MatchService _service = new MatchService(_serviceHub);

            _service.PlayMatch().Wait();
            Console.ReadKey();
        }
    }
}
