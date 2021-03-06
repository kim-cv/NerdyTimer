﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NerdyTimer.Annotations;

namespace NerdyTimer.Model
{
    public class Project : INotifyPropertyChanged
    {
        private string _name;

        public Project(string name)
        {
            _name = name;

            Tasks = new ItemsChangeObservableCollection<ProjectTask>();
        }

        public ItemsChangeObservableCollection<ProjectTask> Tasks { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public void AddTask(string name)
        {
            Tasks.Add(new ProjectTask(name));
        }
        public void AddTask(string name, string elapsedTime)
        {
            Tasks.Add(new ProjectTask(name, elapsedTime));
        }
        public void AddTask(ProjectTask task)
        {
            Tasks.Add(task);
        }

        public override string ToString()
        {
            return Name;
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