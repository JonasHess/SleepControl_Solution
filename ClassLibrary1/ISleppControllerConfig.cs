using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepControll_Lib
{
    public interface ISleppControllerConfig
    {
        // Network Traffic
        bool checkForNetworkTraffic { get; }
        double timeoutInMinutes {get;}
        double SchwellwertRecieved { get; }
        double SchwellwertSent { get; }


    }
}
