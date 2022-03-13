using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_Stopwatches.Model
{
    class DateTab : Tab
    {
        Page frame;
        public Page Frame { get { return frame; } }
        public DateTab()
        {
            Id += 1;
            Name = $"Секундомер {Id} {DateTime.Now.ToLongTimeString()} ";
            frame= new View.Timer();
        }
    }
}
