using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arneb.WebJobs.Live.Models.LiveServices
{
    public class BoxScorePlayer: Player
    {

        private decimal _minutes;
        public decimal Minutes
        { get
            { return Math.Round(_minutes); }
          set
            {
                _minutes = value;
            }
        }
        public int Points { get; set; }
        public int FieldGoalsAttempted { get; set; }
        public int FieldGoalsMade { get; set; }
        public int Assists { get; set; }
        public int ReboundsTotal { get; set; }
        public int Blocks { get; set; }
        public int Steals { get; set; }
        public int ThreePointersAttempted { get; set; }
        public int ThreePointersMade { get; set; }
        public int FreeThrowsAttempted { get; set; }
        public int FreeThrowsMade { get; set; }
        public int Turnovers { get; set; }
        public int PlusMinusPoints { get; set; }
        public int FoulsPersonal { get; set; }
        public string FieldGoals { get { return FieldGoalsMade.ToString() + "/" + FieldGoalsAttempted.ToString(); } }
        public string ThreePointers { get { return ThreePointersMade.ToString() + "/" + ThreePointersAttempted.ToString(); } }
        public string FreeThrows { get { return FreeThrowsMade.ToString() + "/" + FreeThrowsAttempted.ToString(); } }


    }
}
