using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net;
//using System.Net.Http;
using System.Web.Http;
//using MongoDB;
//using MongoDB.Bson;
//using MongoDB.Bson.IO;
//using MongoDB.Bson.Serialization;
//using MongoDB.Driver;
//using MongoDB.Driver.Linq;
//using MongoDB.Shared;
using System.IO;
using MongoRepository;
using System.Web.Http.Cors;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;


namespace pangramWebService
{
        [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PangramController : ApiController
    {


        // GET: api/Pangram
        [HttpGet]
        [Route("pangrams")]
        public GetPangramResponse GetPangramsLimit(int limit)
        {
            IRepository<Pangram> _pangramRepo = new MongoRepository<Pangram>();
            GetPangramResponse getPangramResponse = new GetPangramResponse();
            if (limit > 0)
            {
                var pangrams = _pangramRepo.Select(p => p)
                    .Take(limit)
                    .ToList();

                if (pangrams.Count() > 0)
                {
                    foreach (var pa in pangrams)
                    {
                        getPangramResponse.pangrams.Add(pa.Sentence);
                    }
                }
            }
              return new GetPangramResponse()
                {
                    Content = new JsonContent2(
                    getPangramResponse
                )
                };

        }


        [HttpGet]
        [Route("pangrams")]
        public GetPangramResponse GetPangrams()
        {
            IRepository<Pangram> _pangramRepo = new MongoRepository<Pangram>();
            GetPangramResponse getPangramResponse = new GetPangramResponse();
            var pangrams = _pangramRepo.Select(p => p).ToList();
            if (pangrams.Count() > 0)
            {
                foreach (var pa in pangrams)
                {
                    getPangramResponse.pangrams.Add(pa.Sentence);
                }
            }

            return new GetPangramResponse()
            {
                Content = new JsonContent2(
                getPangramResponse
            )
            };
        }



        //        POST: api/Pangram
        [HttpPost]
        [Route("pangrams")]
        public HttpResponseMessage PostPangram([FromBody]string sentence)
        {
            CreatePangramResponse createPangramResponse = new CreatePangramResponse();
            createPangramResponse.isPangram = false;
            IRepository<Pangram> _pangramRepo = new MongoRepository<Pangram>();

            if (!String.IsNullOrEmpty(sentence))
            {
                var pangram = new Pangram(sentence);
                if (pangram.isPangram)
                {
                    var pangrams = _pangramRepo.Select(p => p).ToList();
                    var checkForSamePangramInRepo = pangrams.AsEnumerable().Where(p => p.Sentence == sentence).Select(p => p.Sentence).ToList();
                    if (checkForSamePangramInRepo.Count() == 0)
                    {
                        _pangramRepo.Add(pangram);
                    }
                    createPangramResponse.isPangram = true;
                }
            }

            return new HttpResponseMessage()
            {
                Content = new JsonContent2(
                    createPangramResponse
                 )
            };

        }


    }
}
