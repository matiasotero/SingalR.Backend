using Match.Football.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match.Football
{
    class Program
    {
        static void Main(string[] args)
        {
            MatchService.PlayMatch().Wait();
            Console.ReadKey();
        }
    }
}
