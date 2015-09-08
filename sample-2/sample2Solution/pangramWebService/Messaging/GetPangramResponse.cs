using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using MongoRepository;
using Newtonsoft.Json;

namespace pangramWebService
{
    [Serializable]
       [JsonObject(MemberSerialization.OptIn)]
    public class GetPangramResponse : HttpResponseMessage
    {
        [JsonProperty]
        public List<string> pangrams { get; set; }

        [JsonConstructor]
        public GetPangramResponse()
        {
            pangrams = new List<string>();
        }
    }

}