using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadInformationAPI;

namespace SilkroadInformationAPI
{
    public interface IPluginInterface
    {
        string PluginName { get; }

        void Initialize();
    }
}
