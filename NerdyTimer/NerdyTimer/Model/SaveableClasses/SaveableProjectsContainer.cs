using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdyTimer.Model
{
    [Serializable]
    class SaveableProjectsContainer
    {
        public List<SaveableProject> Projects = new List<SaveableProject>();
    }
}
