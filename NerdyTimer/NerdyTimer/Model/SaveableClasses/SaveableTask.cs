using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdyTimer.Model
{
    [Serializable]
    class SaveableTask
    {
        private string _time = "";
        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }


        public SaveableTask(string name)
        {
            _name = name;
        }
        public SaveableTask(string name, string elapsedTime)
        {
            _name = name;
            _time = elapsedTime;
        }
    }
}
