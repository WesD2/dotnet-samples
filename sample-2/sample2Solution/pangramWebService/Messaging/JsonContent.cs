using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http.Headers;

namespace pangramWebService
{

    public class JsonContent : HttpContent
    {
        private readonly JToken _value;

        public JsonContent(JToken value)
        {
            _value = value;
            Headers.ContentType = new MediaTypeHeaderValue("application/bson");
        }

        protected override Task SerializeToStreamAsync(Stream stream,
            TransportContext context)
        {
            var jw = new JsonTextWriter(new StreamWriter(stream))
            {
                Formatting = Formatting.Indented
            };
            _value.WriteTo(jw);
            jw.Flush();
            return Task.FromResult<object>(null);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }
    }

    public class JsonContent2 : HttpContent
    {
        private readonly MemoryStream _Stream = new MemoryStream();

        public JsonContent2(object value)
        {
            Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var jw = new JsonTextWriter( new StreamWriter(_Stream));
            jw.Formatting = Formatting.Indented;
            var serializer = new JsonSerializer();
            serializer.Serialize(jw, value);
            jw.Flush();
            _Stream.Position = 0;
        }

    protected override Task SerializeToStreamAsync(Stream stream, TransportContext context) {
        return _Stream.CopyToAsync(stream);
    }

    protected override bool TryComputeLength(out long length) {
        length = _Stream.Length;
        return true;
    }
    }
}