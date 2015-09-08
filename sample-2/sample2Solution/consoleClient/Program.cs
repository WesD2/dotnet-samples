using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pangramWebService;
using MongoRepository;
using pangramWebService;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.IO;

namespace consoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 40);
            Console.Title = "Pangram Web Service";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" ");
            Console.WriteLine("Welcome to the Pangram Web Service");
            Console.WriteLine(" ");
            Console.WriteLine("Enter sentence to be posted to Pangram Web Service");
            Console.WriteLine("Return value is json object  { \"isPangram\": [true / false] } indicating if entered sentence is a pangram");
            Console.WriteLine(" ");

            string sentence = Console.ReadLine();
            var pangramServiceAddress = new Uri("http://localhost/api/pangrams");
            bool isEmpty = false;


            while (!String.IsNullOrEmpty(sentence))
            {

                CreatePangramRequest createPangramRequest = new CreatePangramRequest(HttpMethod.Post, pangramServiceAddress);
                createPangramRequest.Sentence = sentence;

                var content = new JsonContent2(
                    sentence
                 );


                using (HttpClient targetclient = new HttpClient())
                {
                    targetclient.BaseAddress = pangramServiceAddress;
                    targetclient.DefaultRequestHeaders.Accept.Clear();
                    targetclient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");

                    var rez0 = targetclient.PostAsync(pangramServiceAddress, content).Result;
                    CreatePangramResponse getresponse0 = new CreatePangramResponse();
                    if (rez0.IsSuccessStatusCode)
                    {
                        string responseBodyAsText0 = rez0.Content.ReadAsStringAsync().Result;

                        getresponse0 = JsonConvert.DeserializeObject<CreatePangramResponse>(responseBodyAsText0);

                        Console.WriteLine("isPangram = " + getresponse0.isPangram.ToString());

                    }


                    HttpResponseMessage response = new HttpResponseMessage();
                    GetPangramResponse getresponse = new GetPangramResponse();
                    var rez = targetclient.GetAsync(pangramServiceAddress, HttpCompletionOption.ResponseHeadersRead).Result;
                    if (rez.IsSuccessStatusCode)
                    {
                        string responseBodyAsText = rez.Content.ReadAsStringAsync().Result;
                        getresponse = JsonConvert.DeserializeObject<GetPangramResponse>(responseBodyAsText);
                    }


                    Console.WriteLine("Pangrams currently in Mongo Pangram Repository:");
                    if (getresponse.pangrams.Count() == 0)
                    {
                        Console.WriteLine("No pangrams in pangram repository");
                    }
                    else
                    {
                        foreach (var pg in getresponse.pangrams)
                        {
                            Console.WriteLine("  " + pg);
                        }
                    }

                }

                Console.WriteLine(" ");
                Console.WriteLine("Post another sentence to Pangram Web Service");
                sentence = Console.ReadLine();

            }
        }

        static async Task MainAsync(HttpClient targetclient, Uri pangramServiceAddress, CreatePangramRequest createPangramRequest)
        {
            PangramController service = new PangramController();

            HttpResponseMessage response = new HttpResponseMessage();
            GetPangramResponse getresponse = new GetPangramResponse();
            var rez = await targetclient.GetAsync(pangramServiceAddress, HttpCompletionOption.ResponseHeadersRead);

            if (rez.IsSuccessStatusCode)
            {
                string responseBodyAsText = rez.Content.ReadAsStringAsync().Result;

                var jsons = JsonConvert.DeserializeObject<GetPangramResponse>(responseBodyAsText);

            }

            Console.WriteLine(" ");

        }

    }
}
