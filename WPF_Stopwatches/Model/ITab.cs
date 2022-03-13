using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_Stopwatches
{
    interface ITab
    {
        string Name { get; set; }
        ICommand CloseCommand { get; }
        event EventHandler CloseRequested;
    }
    public abstract class Tab : ITab
    {
        public Tab()
        {
            CloseCommand = new RelayCommand(p => CloseRequested?.Invoke(this, EventArgs.Empty));
        }
        public string Name { get; set; }
        static public int Id { get; set; }

        public ICommand CloseCommand { get; }

        public event EventHandler CloseRequested;
    }
}
