using SleepControll_Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{

    class NetworkTrafficStriker : Strikeable
    {

        public NetworkTrafficStriker(SleepController sleepController) : base(sleepController)
        {

        }

        public override void addAllWorkers()
        {
            this.addWorker(new NetworkTrafficWorker());
        }
    }

    class NetworkTrafficWorker : Worker
    {

        PerformanceCounterCategory performanceCounterCategory;
        PerformanceCounter performanceCounterSent;
        PerformanceCounter performanceCounterReceived;
        

        public NetworkTrafficWorker()
        {
            this.initializeNetworScanner();
        }

        private void initializeNetworScanner() { 
        
            performanceCounterCategory = new PerformanceCounterCategory("Network Interface");
            string instance = performanceCounterCategory.GetInstanceNames()[0]; // 1st NIC !
            performanceCounterSent = new PerformanceCounter("Network Interface", "Bytes Sent/sec", instance);
            performanceCounterReceived = new PerformanceCounter("Network Interface", "Bytes Received/sec", instance);
        }
    
        public double getKbsRecieved()
        {
            return performanceCounterReceived.NextValue() / 1024;
        }

        public double getKbsSent()
        {
            return performanceCounterSent.NextValue() / 1024;
        }


        private int timespan = 20;
        private double maxKbsRecieved;
        private double maxKbsSent;

        protected override void DoWork()
        {
            timespan--;

            if (timespan <= 0)
            {
                timespan = 20;
                this.SystemCanGoToSleep = TrafficIsToLow(maxKbsRecieved, maxKbsSent);

                maxKbsRecieved = 0;
                maxKbsSent = 0;
                
            }

            double KbsRecieved = this.getKbsRecieved();
            double KbsSent = this.getKbsSent();
            if (! TrafficIsToLow(KbsRecieved, KbsSent))
            {
                SystemCanGoToSleep = false;
                timespan = 20;
            }
            if (maxKbsRecieved < KbsRecieved)
            {
                maxKbsRecieved = KbsRecieved;
            }
            if (maxKbsSent < KbsSent)
            {
                maxKbsSent = KbsSent;
            }

            Console.WriteLine(timespan + " :  " + this.getKbsRecieved());
            System.Threading.Thread.Sleep(500);
        }

        private bool TrafficIsToLow (double KbsRecieved, double KbsSent)
        {
            return !(KbsRecieved > 1000 || KbsSent > 1000);
        }
    }
}
