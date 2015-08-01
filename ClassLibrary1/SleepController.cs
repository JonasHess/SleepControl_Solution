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
        private List<Strikeable> strikers;

        public SleepController()
        {
            this.strikers = new List<Strikeable>();
            strikers.Add(new NetworkTrafficStriker(this));

        }

        public void StartSleepControll()
        {
            while(true)
            {
                bool canGoToSleep = true;
                foreach (Strikeable striker in strikers)
                {
                    canGoToSleep = canGoToSleep && striker.SystemCanGoToSleep();
                }
                if (canGoToSleep)
                {
                    this.suspendSystem();
                } 
            }
        }

        public void suspendSystem()
        {
            throw new NotImplementedException();
        }
    }
}

