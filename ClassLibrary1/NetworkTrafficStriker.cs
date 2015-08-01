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

        private PerformanceCounterCategory performanceCounterCategory;
        private PerformanceCounter performanceCounterSent;
        private PerformanceCounter performanceCounterReceived;

        private double timeoutInMinutes = 0.2;
        private int AmountOfTests;
        private const int WaitMillisecBetweenTests = 250;
        private const double SchwellwertRecieved = 1000;
        private const double SchwellwertSent = 1000;

        private int timespan;
        private double maxKbsRecieved;
        private double maxKbsSent;

        public NetworkTrafficWorker()
        {
            this.initializeNetworScanner();

            this.AmountOfTests = (int)(timeoutInMinutes * 60) * (1000 / WaitMillisecBetweenTests);
            timespan = AmountOfTests;
        }

        protected override void DoWork()
        {
            timespan--;

            if (timespan <= 0)
            {
                timespan = AmountOfTests;
                this.SystemCanGoToSleep = TrafficIsToLow(maxKbsRecieved, maxKbsSent);
                maxKbsRecieved = 0;
                maxKbsSent = 0;
            }


            double KbsRecieved = this.getKbsRecieved();
            double KbsSent = this.getKbsSent();
            if (!TrafficIsToLow(KbsRecieved, KbsSent))
            {
                SystemCanGoToSleep = false;
                timespan = AmountOfTests;
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
            System.Threading.Thread.Sleep(WaitMillisecBetweenTests);
        }


        private void initializeNetworScanner()
        {

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




        private bool TrafficIsToLow(double KbsRecieved, double KbsSent)
        {
            return !(KbsRecieved > SchwellwertRecieved || KbsSent > SchwellwertSent);
        }
    }
}
