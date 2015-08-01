using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepControll_Lib
{
    abstract class Worker
    {



        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        private volatile bool _shouldStop = false;

        public bool SystemCanGoToSleep
        {
            get; set;
        }

        // This method will be called when the thread is started.
        public void startthread()
        {
            while (!_shouldStop)
            {
                this.DoWork();

            }
        }
        public void RequestStop()
        {
            _shouldStop = true;
        }

        protected abstract void DoWork();
    }
}
