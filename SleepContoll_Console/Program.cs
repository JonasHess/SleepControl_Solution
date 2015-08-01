using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace SleepContoll_Console
{
    class Program
    {
        static void Main(string[] args)
        {

            ClassLibrary1.SleepController s = new ClassLibrary1.SleepController();
            s.StartSleepControll();
        }
    }
}
