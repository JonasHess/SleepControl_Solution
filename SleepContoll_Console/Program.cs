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


            
            Thread server = new Thread(SynchronousSocketListener.StartListening);
            Thread client1 = new Thread(SynchronousSocketClient.StartClient);
            Thread client2 = new Thread(SynchronousSocketClient.StartClient);
            Thread client3 = new Thread(SynchronousSocketClient.StartClient);
            Thread client4 = new Thread(SynchronousSocketClient.StartClient);
            Thread client5 = new Thread(SynchronousSocketClient.StartClient);

            server.Start();
            System.Threading.Thread.Sleep(500);

            client1.Start();
            client2.Start();
            //client3.Start();
            //client4.Start();
            //client5.Start();



            client1.Join();
            client2.Join();
            //client3.Join();
            //client4.Join();
            //client5.Join();
            server.Join();
        


        }
    }
}
