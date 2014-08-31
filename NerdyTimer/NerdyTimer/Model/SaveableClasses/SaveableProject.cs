using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace NerdyTimer.Model
{
    [Serializable]
    class SaveableProject
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public List<SaveableTask> Tasks { get; set; }

        public SaveableProject(string name)
        {
            _name = name;

            Tasks = new List<SaveableTask>();
        }

        public void AddTask(string name)
        {
            Tasks.Add(new SaveableTask(name));
        }
        public void AddTask(string name, string elapsedTime)
        {
            Tasks.Add(new SaveableTask(name, elapsedTime));
        }
        public void AddTask(SaveableTask task)
        {
            Tasks.Add(task);
        }
    }
}
