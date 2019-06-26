using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading;

namespace RandomQuoteConsole
{
    class Program
    {
        const string apiUrl = "https://api.quotable.io/random";

        static void Main(string[] args)
        {
            Console.WriteLine("\n\n\n");

            for (; ; )
            {
                PrintRandomQuote();
                Thread.Sleep(5 * 60000); // 5 minutes
            }

            Console.ReadKey();
        }

        private static void timer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            PrintRandomQuote();
        }

        private static void PrintRandomQuote()
        {
            Quote quote = FetchRandomQuote();

            Console.WriteLine($@"""{ quote.content }"" { quote.author }");
            Console.WriteLine("\n\n\n");
        }

        private static Quote FetchRandomQuote()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string jsonResponse = reader.ReadToEnd();

                return JsonConvert.DeserializeObject<Quote>(jsonResponse);
            }
        }

        public class Quote
        {
            public string content;
            public string author;
        }
    }
}
