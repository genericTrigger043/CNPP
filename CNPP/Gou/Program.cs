using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Net;
using System.ComponentModel;
using System.Threading;
using System.Net.NetworkInformation;

namespace Gou
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0){
                throw new ArgumentException("Enter the ip address: ");
            }
            string adressWWW = args[0];
            AutoResetEvent waiter = new AutoResetEvent(false);

            Ping pingSender = new System.Net.NetworkInformation.Ping();

            pingSender.PingCompleted += new PingCompletedEventHandler(PingCompletedCallback);

            string data = "AAA!";
            byte[] byteBuffer = Encoding.ASCII.GetBytes(data);

            int timeout = 3600;

            PingOptions options = new PingOptions(64, true);

            Console.WriteLine("Time to live: {0}", options.Ttl);
            Console.WriteLine("Do not fragment packages: {0}", options.DontFragment);

            pingSender.SendAsync(adressWWW, timeout, byteBuffer, options, waiter);

            waiter.WaitOne();
            Console.WriteLine("The ping command has ended.");
            Console.ReadKey();

        }

        private static void PingCompletedCallback(object sender, PingCompletedEventArgs e)
        {
            throw new NotImplementedException();

            if(e.Cancelled){
                Console.WriteLine("Ping canceled.");

                ((AutoResetEvent)e.UserState).Set();
            }

            if(e.Error != null){
                Console.WriteLine("Ping Error: ");
                Console.WriteLine(e.Error.ToString());

                ((AutoResetEvent)e.UserState).Set();
            }

            PingReply answer = e.Reply;

            dpa(answer);

            ((AutoResetEvent)e.UserState).Set();


        }

        private static void dpa(PingReply answer)
        {
            throw new NotImplementedException();

            if(answer == null)
                return;

            Console.WriteLine("Ping status: {0}", answer.Status);
            if(answer.Status == IPStatus.Success){
                Console.WriteLine("Adress IP: {0}", answer.Address.ToString());
                Console.WriteLine("Time to live: {0}", answer.Options.Ttl);
                Console.WriteLine("It is not fragmented: {0}", answer.Options.DontFragment);
                Console.WriteLine("The size of Buffer: {0}", answer.Buffer.Length);
            }

        }
    }
}
