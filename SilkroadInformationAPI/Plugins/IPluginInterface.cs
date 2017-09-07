using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilkroadInformationAPI;
using System.Windows.Forms;
namespace SilkroadInformationAPI.PluginInterface
{
    public interface IPluginInterface
    {
        string PluginName { get; }

        TabPage Initialize(Dictionary<string, int> SharedVariables);
    }
}
