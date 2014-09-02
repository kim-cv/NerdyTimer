using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NerdyTimer.Annotations;
using NerdyTimer.Model;
using Task = NerdyTimer.Model.Task;

namespace NerdyTimer.ViewModel
{
    public class ProjectViewModel : INotifyPropertyChanged
    {
        public FileHandler FileHandler { get; set; }

        public TaskTimer TaskTimer { get; set; }

        public ItemsChangeObservableCollection<Project> Projects {get; set;}


        private Project _selectedProject;
        public Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                BtnAddTaskIsEnabled = true;
                OnPropertyChanged();
            }
        }

        private Task _selectedTask;
        public Task SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                BtnStartTimerIsEnabled = true;
                OnPropertyChanged();
            }
        }

        #region ICommands
        private ICommand _btnCommand_AddProject;
        public ICommand BtnCommandAddProject
        {
            get
            {
                return _btnCommand_AddProject;
            }
            set
            {
                _btnCommand_AddProject = value;
            }
        }

        private ICommand _btnCommand_AddTask;
        public ICommand BtnCommandAddTask
        {
            get
            {
                return _btnCommand_AddTask;
            }
            set
            {
                _btnCommand_AddTask = value;
            }
        }

        private ICommand _btnCommand_StartTimer;
        public ICommand BtnCommandStartTimer
        {
            get
            {
                return _btnCommand_StartTimer;
            }
            set
            {
                _btnCommand_StartTimer = value;
            }
        }

        private ICommand _btnCommand_StopTimer;
        public ICommand BtnCommandStopTimer
        {
            get
            {
                return _btnCommand_StopTimer;
            }
            set
            {
                _btnCommand_StopTimer = value;
            }
        }
        #endregion

        #region Methods for ICommands
        //Method for Add Project ICommand
        public void AddProject(Object obj)
        {
            TextBox tb = (TextBox)obj;
            Projects.Add(new Project(tb.Text));

            //Save Projects and Tasks
            FileHandler.SaveProjects(Projects);
        }
        //Method for Add Task ICommand
        public void AddTask(Object obj)
        {
            TextBox tb = (TextBox)obj;
            SelectedProject.AddTask(tb.Text);

            //Save Projects and Tasks
            FileHandler.SaveProjects(Projects);
        }
        //Method for Start timer ICommand
        public void StartTimer(Object obj)
        {
            TaskTimer.Start();
            BtnStartTimerIsEnabled = false;
            BtnStopTimerIsEnabled = true;
        }
        //Method for Stop timer ICommand
        public void StopTimer(Object obj)
        {
            //Elapsed time, update selected task
            Label l = (Label)obj;
            SelectedTask.Time = l.Content.ToString();
            
            TaskTimer.Stop();

            BtnStartTimerIsEnabled = true;
            BtnStopTimerIsEnabled = false;

            //Save Projects and Tasks
            FileHandler.SaveProjects(Projects);
        }
        #endregion
        
        #region Booleans to decide if buttons are enabled or not
        private bool _btnAddProjectIsEnabled = true;
        public bool BtnAddProjectIsEnabled
        {
            get { return _btnAddProjectIsEnabled; }
            set
            {
                _btnAddProjectIsEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _btnAddTaskIsEnabled;
        public bool BtnAddTaskIsEnabled
        {
            get { return _btnAddTaskIsEnabled; }
            set
            {
                _btnAddTaskIsEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _btnStartTimerIsEnabled;
        public bool BtnStartTimerIsEnabled
        {
            get { return _btnStartTimerIsEnabled; }
            set
            {
                _btnStartTimerIsEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _btnStopTimerIsEnabled;
        public bool BtnStopTimerIsEnabled
        {
            get { return _btnStopTimerIsEnabled; }
            set
            {
                _btnStopTimerIsEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ProjectViewModel()
        {
            FileHandler = new FileHandler();

            Projects = new ItemsChangeObservableCollection<Project>()
            {
                /*Dummy Data
                new Project("Project1"),
                new Project("Project2"),
                new Project("Project3")
                */
            };

            try
            {
                Projects = FileHandler.LoadProjects();
            }
            catch (Exception e)
            {
            }

            TaskTimer = new TaskTimer();



            BtnCommandAddProject = new RelayCommand(new Action<object>(AddProject));
            BtnCommandAddTask = new RelayCommand(new Action<object>(AddTask));
            BtnCommandStartTimer = new RelayCommand(new Action<object>(StartTimer));
            BtnCommandStopTimer = new RelayCommand(new Action<object>(StopTimer));
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
