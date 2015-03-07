using EventSource4Net;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSEConsole
{
    class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            
            Console.WriteLine("Enter the Spark Access Token:");
            string accessToken = Console.ReadLine();

            EventSource es = new EventSource(new Uri("https://api.spark.io/v1/devices/events?access_token=" + accessToken), 10000);
            es.StateChanged += es_StateChanged;
            es.EventReceived += es_EventReceived;
            es.Start(new System.Threading.CancellationToken());

            Console.ReadLine();

        }

        static void es_EventReceived(object sender, ServerSentEventReceivedEventArgs e)
        {
            
            _log.Info(string.Format("Event: {0} Data: {1}", e.Message.EventType,e.Message.Data));
        }

        static void es_StateChanged(object sender, StateChangedEventArgs e)
        {
            _log.Info(string.Format("State: {0}", e.State));

        }

    }
}
