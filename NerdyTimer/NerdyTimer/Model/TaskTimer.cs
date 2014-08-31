using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NerdyTimer.Annotations;
using Timer = System.Windows.Forms.Timer;

namespace NerdyTimer.Model
{
    public class TaskTimer : INotifyPropertyChanged
    {
        private Timer _timer;
        private Stopwatch _stopwatch;

        private string _elapsedTime;
        public string ElapsedTime
        {
            get { return _elapsedTime; }
            set
            {
                _elapsedTime = value;
                OnPropertyChanged();
            }
        }

        public TaskTimer()
        {
            _stopwatch = new Stopwatch();

            _timer = new Timer();
            _timer.Interval = 5;
            _timer.Tick += new EventHandler(OnTimedEvent);
            _timer.Enabled = true;            
        }


        public void Start()
        {
            _timer.Start();
            _stopwatch.Start();            
        }

        public void Pause()
        {
            _timer.Stop();
            _stopwatch.Stop();
        }

        public void Stop()
        {
            _timer.Stop();
            _stopwatch.Stop();
            _stopwatch.Reset();

            ResetElapsedTime();
        }

        private void ResetElapsedTime()
        {
            ElapsedTime = String.Format("00:00:00");
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            TimeSpan timeSpan = _stopwatch.Elapsed;
            Byte Hours = (Byte)timeSpan.Hours;
            Byte Minutes = (Byte)timeSpan.Minutes;
            Byte Seconds = (Byte)timeSpan.Seconds;

            ElapsedTime = String.Format("{0:00}:{1:00}:{2:00}", Hours, Minutes, Seconds);
        }




        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
