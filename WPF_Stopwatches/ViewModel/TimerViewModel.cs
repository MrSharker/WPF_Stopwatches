using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WPF_Stopwatches.Model;


namespace WPF_Stopwatches.ViewModel
{
    class TimerViewModel : INotifyPropertyChanged
    {
        private DispatcherTimer dt = new DispatcherTimer();
        private Stopwatch sw = new Stopwatch();
        string currentTime = "00:00,00";

        public event PropertyChangedEventHandler PropertyChanged;
        private Visibility btnStart = Visibility.Visible;
        private Visibility btnStop = Visibility.Hidden;
        private Visibility btnReset = Visibility.Hidden;
        private Visibility btnContinue = Visibility.Hidden;
        private bool resetEnable = false;

        public Visibility BtnStart { get { return btnStart; } set { btnStart = value; OnPropertyChanged("BtnStart"); } }
        public Visibility BtnStop { get { return btnStop; } set { btnStop = value; OnPropertyChanged("BtnStop"); } }
        public Visibility BtnReset { get => btnReset; set { btnReset = value; OnPropertyChanged("BtnReset"); } }
        public Visibility BtnContinue { get => btnContinue; set { btnContinue = value; OnPropertyChanged("BtnContinue"); } }
        public bool ResetEnable { get => resetEnable; set { resetEnable = value; OnPropertyChanged("ResetEnable"); } }
        public ICommand StartTimerCommand { get; }
        public ICommand StopTimerCommand { get; }
        public ICommand ResetTimerCommand { get; }
        public ICommand ContinueTimerCommand { get; }
        public string CurrentTime { 
            get 
            { 
                return currentTime;
            }
            set
            {
                currentTime = value;
                OnPropertyChanged("CurrentTime");
            } 
        }

        public TimerViewModel()
        {
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
            StartTimerCommand = new RelayCommand(p => Startbtn_Click());
            StopTimerCommand = new RelayCommand(p => Stopbtn_Click());
            ResetTimerCommand = new RelayCommand(p => Resetbtn_Click());
            ContinueTimerCommand = new RelayCommand(p => Continuebtn_Click());
        }
        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                TimeSpan ts = sw.Elapsed;
                CurrentTime = String.Format("{0:00}:{1:00},{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            }
        }

        private void Startbtn_Click()
        {
            sw.Start();
            dt.Start();
            BtnStart = Visibility.Hidden;
            BtnStop = Visibility.Visible;
            BtnReset = Visibility.Visible;
        }

        private void Stopbtn_Click()
        {
            if (sw.IsRunning)
            {
                BtnStop = Visibility.Hidden;
                BtnContinue = Visibility.Visible;
                ResetEnable = true;
                sw.Stop();
            }
        }

        private void Continuebtn_Click()
        {
            BtnStop = Visibility.Visible;
            BtnContinue = Visibility.Hidden;
            ResetEnable = false;
            Startbtn_Click();
        }
        private void Resetbtn_Click()
        {
            sw.Reset();
            CurrentTime = "00:00,00";
            BtnStop = Visibility.Hidden;
            BtnReset = Visibility.Hidden;
            BtnContinue = Visibility.Hidden;
            BtnStart = Visibility.Visible;
            ResetEnable = false;
        }
    }
}
