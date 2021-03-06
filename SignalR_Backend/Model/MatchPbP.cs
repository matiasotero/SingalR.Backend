﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Backend.Model
{
    public class MatchPbP
    {
        [JsonProperty("playerName")]
        public string PlayerName { get; set; }

        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("actionNumber")]
        public int ActionNumber { get; set; }

        [JsonProperty("sendDelay")]
        public double sendDelay { get; set; }

        [JsonProperty("success")]
        public int Success { get; set; }

        [JsonProperty("previousAction")]
        public int PreviousAction { get; set; }

        [JsonProperty("messageId")]
        public int MessageId { get; set; }

        [JsonProperty("periodType")]
        public string PeriodType { get; set; }

        [JsonProperty("score2")]
        public string Score2 { get; set; }

        [JsonProperty("score1")]
        public string Score1 { get; set; }

        [JsonProperty("won")]
        public string Won { get; set; }

        [JsonProperty("timeActual")]
        public string TimeActual { get; set; }

        [JsonProperty("shotClock")]
        public string ShotClock { get; set; }

        [JsonProperty("TeamNumber")]
        public int TeamNumber { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("clock")]
        public string Clock { get; set; }

        [JsonProperty("jumpball")]
        public string Jumpball { get; set; }
    }
}
