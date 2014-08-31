using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace NerdyTimer.Model
{
    /*
     * This class handles everyting concerned with saving and loading projects and tasks.
     * 
     * Due to problems serializing my Project and Task classes because you cannot serialize the 
     * public event PropertyChangedEventHandler PropertyChanged;
     * You can omit this field by using [field: NonSerialized]
     * But the problem with that is whenever you load in your Projects/Tasks, they are not subscribed to the
     * PropertyChangedEventHandler and they will not talk to your ViewModel->View.
     * 
     * My quick-fix is to make a serializable SaveableProjectsContainer with all the projects and tasks and
     * on the saving mechanism I loop through all the Projects and take their info into the SaveableProject and the same with Tasks
     * 
     * The loading mechanism is just reversing the algorithm
     */
    public class FileHandler
    {
        private IFormatter BinaryFormatter;
        private FileStream FileStream;

        public FileHandler()
        {
            BinaryFormatter = new BinaryFormatter();
        }

        public void SaveProjects(ICollection<Project> Projects)
        {
            //Save mechanism
            SaveableProjectsContainer SaveableProjects = new SaveableProjectsContainer();
            foreach (Project project in Projects)
            {
                SaveableProjects.Projects.Add(new SaveableProject(project.Name));
                foreach (Task task in project.Tasks)
                {
                    SaveableProjects.Projects.Last().AddTask(new SaveableTask(task.Name, task.Time));
                }
            }

            //Write to file
            Stream stream = new FileStream("NerdyTimer_Savings.txt", FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryFormatter.Serialize(stream, SaveableProjects);
            stream.Close();
        }

        public ItemsChangeObservableCollection<Project> LoadProjects()
        {
            //Read from file
            Stream stream = new FileStream("NerdyTimer_Savings.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            SaveableProjectsContainer SaveableProjectsContainer = (SaveableProjectsContainer)BinaryFormatter.Deserialize(stream);

            //Load mechanism
            ItemsChangeObservableCollection<Project> loadedProjects = new ItemsChangeObservableCollection<Project>();
            foreach (SaveableProject SavedProject in SaveableProjectsContainer.Projects)
            {
                loadedProjects.Add(new Project(SavedProject.Name));
                foreach (SaveableTask SavedTask in SavedProject.Tasks)
                {
                    loadedProjects.Last().AddTask(new Task(SavedTask.Name, SavedTask.Time));
                }
            }

            stream.Close();
            return loadedProjects;
        }
    }
}
