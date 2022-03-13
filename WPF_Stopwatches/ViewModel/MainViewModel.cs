using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using WPF_Stopwatches;
using WPF_Stopwatches.Model;
using System.Collections.Specialized;

namespace WPF_Stopwatches.ViewModel
{
    class MainViewModel
    {
        public MainViewModel()
        {
            NewTabCommand = new RelayCommand(p => NewTab());
            tabs = new ObservableCollection<ITab>();
            tabs.Add(new DateTab());
            tabs.CollectionChanged += Tabs_ColleсtionChanged;
            Tabs = tabs;
        }
        private readonly ObservableCollection<ITab> tabs;
        public ICommand NewTabCommand { get; }
        public ICollection<ITab> Tabs { get; }
        private void NewTab()
        {
            if (Tabs.Count != 10)
            {
                Tabs.Add(new DateTab());
            }
        }
        private void Tabs_ColleсtionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ITab tab;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    tab = (ITab)e.NewItems[0];
                    tab.CloseRequested += OnTabCloseRequested;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    tab = (ITab)e.OldItems[0];
                    tab.CloseRequested -= OnTabCloseRequested;
                    break;
            }
        }

        private void OnTabCloseRequested(object sender, EventArgs e)
        {
            if (Tabs.Count>1)
                Tabs.Remove((ITab)sender);
        }
    }
}
