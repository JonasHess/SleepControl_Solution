using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
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


        }

        private SemaphoreSlim s = new SemaphoreSlim(0);


        public void StartSleepControll()
        {
            this.strikers = new List<Strikeable>();
            strikers.Add(new NetworkTrafficStriker(this));

            while (true)
            {
                s.Wait();
                bool canGoToSleep = true;
                foreach (Strikeable striker in strikers)
                {
                    canGoToSleep = canGoToSleep && striker.SystemCanGoToSleep();
                }
                if (canGoToSleep)
                {
                    foreach (Strikeable striker in strikers)
                    {
                        striker.stop();
                    }
                    strikers.Clear();
                    this.suspendSystem();
                    break;
                }
            }

        }

        public void informAboutStateChange()
        {
            s.Release();
        }

        [DllImport("Powrprof.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

        public void suspendSystem()
        {
            //throw new NotImplementedException();

            // Hibernate
            // SetSuspendState(true, true, true);
            // Standby
            //SetSuspendState(false, true, true);

        }
    }
}

