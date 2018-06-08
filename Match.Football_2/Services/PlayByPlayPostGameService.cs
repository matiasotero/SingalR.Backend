using Arneb.WebJobs.Live.Models.LiveServices;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arneb.WebJobs.Live.Services
{
    public class PlayByPlayPostGameService
    {
        public static async Task SavePlayByPlay(int matchId, PlayByPlayMessage playByPlayMessage )
        {
            var x = new PlayByPlayPostGame { MatchId = matchId, AllActions = playByPlayMessage.AllActions };

            var pbpMessageSerialized = JsonConvert.SerializeObject(x);
            var data = JObject.Parse(pbpMessageSerialized);

            DocumentClient _client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["endPointUri"]), ConfigurationManager.AppSettings["authKeyString"]);

            await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("PostGame", "PlayByPlay"), data);
        }


    }

    public class PlayByPlayPostGame
    {
        [JsonProperty("id")]
        public string Id { get { return MatchId.ToString(); } }

        public int MatchId { get; set; }

        public List<ActionPlayer> AllActions { get; set; }

    }
}
