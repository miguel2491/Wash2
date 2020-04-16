using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Timers;
using Xamarin.Forms;

namespace Wash2.Models
{
    public class TimerModel : INotifyPropertyChanged
    {
        Stopwatch stopwatch = new Stopwatch();
        private Timer time = new Timer();

        private string _StopWatchHours;
        public string StopWatchHours
        {
            get { return _StopWatchHours; }
            set
            {
                _StopWatchHours = value;
                OnPropertyChanged("StopWatchHours");
            }
        }

        private string _StopWatchMinutes;
        public string StopWatchMinutes
        {
            get { return _StopWatchMinutes; }
            set
            {
                _StopWatchMinutes = value;
                OnPropertyChanged("StopWatchMinutes");
            }
        }

        private string _StopWatchSeconds;
        public string StopWatchSeconds
        {
            get { return _StopWatchSeconds; }
            set
            {
                _StopWatchSeconds = value;
                OnPropertyChanged("StopWatchSeconds");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        public TimerModel()
        {

            stopwatch.Start();
            StopWatchHours = stopwatch.Elapsed.Hours.ToString();
            StopWatchMinutes = stopwatch.Elapsed.Minutes.ToString();
            StopWatchSeconds = stopwatch.Elapsed.Seconds.ToString();

            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                StopWatchHours = stopwatch.Elapsed.Hours.ToString();
                StopWatchMinutes = stopwatch.Elapsed.Minutes.ToString();
                StopWatchSeconds = stopwatch.Elapsed.Seconds.ToString();
                return true;
            });
        }
    }
}
