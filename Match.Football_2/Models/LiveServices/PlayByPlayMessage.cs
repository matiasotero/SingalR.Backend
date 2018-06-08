using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arneb.WebJobs.Live.Models.LiveServices
{
    public sealed class PlayByPlayMessage
    {
        private static PlayByPlayMessage instance = null;
        private static readonly object padlock = new object();

        private PlayByPlayMessage()
        {
            AllActions = new List<ActionPlayer>();

        }

        public static PlayByPlayMessage Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new PlayByPlayMessage();
                    }
                    return instance;
                }
            }
        }

        public List<ActionPlayer> AllActions { get; set; }
    }
}
