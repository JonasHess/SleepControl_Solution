using SleepControll_Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepController_Lib
{

    class NetworkTrafficStriker : Strikeable
    {



        public NetworkTrafficStriker(SleepController sleepController) : base(sleepController)
        {
           
        }

        public override void addAllWorkers()
        {
            ISleppControllerConfig config = SleepController.config;
            this.addWorker(new NetworkTrafficWorker(config.timeoutInMinutes, config.SchwellwertRecieved, config.SchwellwertRecieved));
        }
    }



    class NetworkTrafficWorker : Worker
    {

        private PerformanceCounterCategory performanceCounterCategory;
        private PerformanceCounter performanceCounterSent;
        private PerformanceCounter performanceCounterReceived;
        
        private int AmountOfTests;
        private const int WaitMillisecBetweenTests = 250;
        private double SchwellwertRecieved = 1000;
        private double SchwellwertSent = 1000;

        private int timespan;
        private double maxKbsRecieved;
        private double maxKbsSent;

        public NetworkTrafficWorker(double timeoutInMinutes, double SchwellwertRecieved, double SchwellwertSent)
        {
            this.SchwellwertRecieved = SchwellwertRecieved;
            this.SchwellwertSent = SchwellwertSent;

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
                this.SystemCanGoToSleep = isTrafficToLow(maxKbsRecieved, maxKbsSent);
                maxKbsRecieved = 0;
                maxKbsSent = 0;
            }


            double KbsRecieved = this.getKbsRecieved();
            double KbsSent = this.getKbsSent();
            if (!isTrafficToLow(KbsRecieved, KbsSent))
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


        private bool isTrafficToLow(double KbsRecieved, double KbsSent)
        {
            return !(KbsRecieved > SchwellwertRecieved || KbsSent > SchwellwertSent);
        }


        private void initializeNetworScanner()
        {

            performanceCounterCategory = new PerformanceCounterCategory("Network Interface");
            string instance = performanceCounterCategory.GetInstanceNames()[0]; // erster NIC ! //TODO 
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
        

        
    }
}
