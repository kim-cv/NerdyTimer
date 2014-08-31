using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NerdyTimer.Annotations;

namespace NerdyTimer.Model
{
    public class Task : INotifyPropertyChanged
    {
        private string _time = "";
        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        
        public Task(string name)
        {
            _name = name;
        }
        public Task(string name, string elapsedTime)
        {
            _name = name;
            _time = elapsedTime;
        }


        public override string ToString()
        {
            return Name + " - " + Time;
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