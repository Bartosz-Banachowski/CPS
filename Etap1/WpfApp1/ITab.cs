using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1
{
    public interface ITab
    {
        string Name { get; set; }
        ICommand CloseCommand { get; }
        event EventHandler CloseRequested;
    }
}
