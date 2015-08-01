using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleepController_Lib;
using SleepControll_Lib.Sockets;
using System.Threading;

namespace SleepContoll_Console
{
    class Program
    {
        static void Main(string[] args)
        {

            //SleepController_Lib.SleepController s = new SleepController_Lib.SleepController();
            //s.StartSleepControll();


            
            Thread server = new Thread(AsynchronousSocketListener.StartListening);
            Thread client = new Thread(AsynchronousClient.StartClient);

            server.Start();
            System.Threading.Thread.Sleep(5000);
            client.Start();
            client.Join();
            server.Join();
        


        }
    }
}
