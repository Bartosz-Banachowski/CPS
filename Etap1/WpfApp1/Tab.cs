using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1
{
    public abstract class Tab : ITab
    {
        public Tab()
        {
            CloseCommand = new DelegateCommand(p => CloseRequested?.Invoke(this, EventArgs.Empty));
        }

        public string Name { get; set; }

        public ICommand CloseCommand { get; }

        public event EventHandler CloseRequested;
    }
}
