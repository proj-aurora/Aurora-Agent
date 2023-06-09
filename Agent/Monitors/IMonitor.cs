using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Monitors
{
    internal interface IMonitor : IDisposable
    {
        const string Name = "place_holder";

        void Initialize();

        void Enable();
    }
}
