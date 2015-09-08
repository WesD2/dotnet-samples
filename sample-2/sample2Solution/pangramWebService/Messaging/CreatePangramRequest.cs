using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;

namespace pangramWebService
{
    public class CreatePangramRequest : HttpRequestMessage
    {
      public string Sentence { get; set; }

      public CreatePangramRequest(string sentence)
      {
          this.Sentence = sentence;
      }

      public CreatePangramRequest()
      {
      }

        public CreatePangramRequest(HttpMethod method, Uri uri, string sentence)
            :base(method, uri)
      {
          this.Sentence = sentence;
      }

        public CreatePangramRequest(HttpMethod method, Uri uri)
            : base(method, uri)
        {
        }
    }
}