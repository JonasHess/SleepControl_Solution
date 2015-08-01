using SleepControll_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SleepController_Lib
{

    struct WorkerThread
    {
        public Worker worker;
        public Thread thread;
    }

    abstract class Strikeable
    {
        private SleepController sleepController;
        private List<WorkerThread> workers;

        public Strikeable(SleepController sleepController)
        {
            this.sleepController = sleepController;
            this.workers = new List<WorkerThread>();
            this.addAllWorkers();
        }

        protected void addWorker(Worker worker)
        {
            WorkerThread wt = new WorkerThread();
            wt.worker = worker;
            wt.worker.striker = this;
            wt.thread = new Thread(worker.startthread);
            workers.Add(wt);
            wt.thread.Start();

        }

        public abstract void addAllWorkers();

        public void stop()
        {
            foreach (WorkerThread wt in this.workers)
            {
                wt.worker.RequestStop();
                wt.thread.Join();
            }
            workers.Clear();
        }

        public void suspendSystem()
        {
            this.sleepController.suspendSystem();
        }

        public void onWorkerChangedState(bool newValue)
        {
            if (newValue)
            {
                this.sleepController.informAboutStateChange();
            }
        }

        public Boolean SystemCanGoToSleep()
        {
            bool canGoToSleep = true;
            foreach (WorkerThread w in workers)
            {
                canGoToSleep = canGoToSleep && w.worker.SystemCanGoToSleep;
            }
            return canGoToSleep;
        }
    }
}
