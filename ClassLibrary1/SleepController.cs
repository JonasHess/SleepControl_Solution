using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class SleepController
    {
    
        



        public static void StartSleepControll()
        {
            
            NetworkTrafficScanner scanner = new NetworkTrafficScanner();


            while (true)
            {

                double recieved = scanner.getKbsRecieved();
                double sent = scanner.getKbsSent();

                Console.WriteLine("sent: " + sent + " Kb/s  |   recieved: " + recieved + " Kb/s");
                Thread.Sleep(500);
            }
            /*
    foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
    {
            float bandwidth = ni.Speed;
            const int numberOfIterations = 1;

            float sendSum = 0;
            float receiveSum = 0;

            float lastRec = ni.GetIPv4Statistics().BytesReceived;
            float lastSent = ni.GetIPv4Statistics().BytesSent;

            System.Threading.Thread.Sleep(1000);
            float br = ni.GetIPv4Statistics().BytesReceived;
            float bs = ni.GetIPv4Statistics().BytesSent;
            receiveSum += br - lastRec;
            sendSum += bs - lastSent;

            float dataSent = sendSum;
            float dataReceived = receiveSum;

            //double dout = (((8 * (dataSent)) / (bandwidth) * 100) * 1024);
            //double din = (((8 * (dataReceived)) / (bandwidth) * 100) * 1024);

            double dout = (8 * (dataSent));
            double din = (8 * (dataReceived));


            dout = Math.Round(dout, 4);
            din = Math.Round(din, 4);

            string doutround = Convert.ToString(dout);
            string dinround = Convert.ToString(din);


            Console.WriteLine("DataIn: " + dinround + "kb/s");
            //Console.WriteLine("DataOut: " + doutround + "kb/s");
            System.Threading.Thread.Sleep(1000);
            */
        }

        public void suspendSystem()
        {
            throw new NotImplementedException();
        }
    }
}

