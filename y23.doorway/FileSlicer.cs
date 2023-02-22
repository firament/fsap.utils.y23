using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sak.utils.FileSlicer;

namespace y23.doorway
{
    internal class FileSlicer
    {
        public void RunTasks()
        {
            RunHello();
        }

        private void RunHello()
        {
            FileSlicerUtils fsu= new FileSlicerUtils();
            fsu.Hello();
        }
    }
}
