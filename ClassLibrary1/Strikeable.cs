using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    abstract class Strikeable
    {
        private SleepController sleepController;

        public Strikeable(SleepController sleepController)
        {
            this.sleepController = sleepController;
        }

        public void suspendSystem()
        {
            this.sleepController.suspendSystem();
        }

        public abstract bool SystemCanGoToSleep();
        
    }
}
