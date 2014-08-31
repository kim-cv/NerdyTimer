using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdyTimer.ViewModel
{
    public class CombinedViewModel
    {
        /*
         * Don't delete
         * 
         * It's not possible to bind multiple ViewModels to a single view, therefore think of this class
         * as a ViewModel container, combining multiple ViewModels, even though right now there is only
         * one ViewModel.
         * 
         * If you decide to delete this ViewModel, your name shall forever be John Derp
         */

        public ProjectViewModel ProjectViewModel { get; set; }

        public CombinedViewModel()
        {
            ProjectViewModel = new ProjectViewModel();
        }
    }
}
