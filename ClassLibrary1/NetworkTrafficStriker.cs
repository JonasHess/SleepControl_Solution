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

        PerformanceCounterCategory performanceCounterCategory;
        PerformanceCounter performanceCounterSent;
        PerformanceCounter performanceCounterReceived;


        public NetworkTrafficStriker(SleepController sleepController) : base(sleepController) 
        {
            initializeNetworScanner();  
        }
        

        private void initializeNetworScanner() { 
        
            performanceCounterCategory = new PerformanceCounterCategory("Network Interface");
            string instance = performanceCounterCategory.GetInstanceNames()[0]; // 1st NIC !
            performanceCounterSent = new PerformanceCounter("Network Interface", "Bytes Sent/sec", instance);
            performanceCounterReceived = new PerformanceCounter("Network Interface", "Bytes Received/sec", instance);
        }


        public override bool SystemCanGoToSleep()
        {
            throw new NotImplementedException();
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
