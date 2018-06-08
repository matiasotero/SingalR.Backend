using System;
using System.Collections.Generic;
using System.Text;

namespace Arneb.WebJobs.Live.Models.LiveServices
{
    public sealed class BoxScoreMessage
    {
        private static BoxScoreMessage instance = null;
        private static readonly object padlock = new object();

        private BoxScoreMessage()
        {
            FirstTeam = new BoxScore();
            SecondTeam = new BoxScore();
        }

        public static BoxScoreMessage Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new BoxScoreMessage();
                    }
                    return instance;
                }
            }
        }

        public BoxScore FirstTeam { get; set; }

        public BoxScore SecondTeam { get; set; }

    }
}