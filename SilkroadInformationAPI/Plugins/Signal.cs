using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkroadInformationAPI.Plugins
{
    public class Signal
    {
        public static event Action OnSignal;
        public static void Rise()
        {
            OnSignal?.Invoke();
        }
    }
}
