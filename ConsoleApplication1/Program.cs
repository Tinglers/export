using System;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                using (HttpResponseMessage webresponse = getDaysTickets(DateTime.Today))
                {
                    HulpverzoekCollection verzoeken = Deserialize<HulpverzoekCollection>(webresponse.ToString());
                }
            }
            

        }

        public static T Deserialize<T>(string json)
        {
            var obj = Activator.CreateInstance<T>();
            var ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            var serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }

        static HttpResponseMessage getDaysTickets(DateTime date)
        {
            var endpoint = "";
            var urlParameters = "";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(endpoint);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("http request succesfull");
                return response;
            }
            else
            {
                Console.WriteLine("http request failed");
                return null;
            }
        }
    }
}
