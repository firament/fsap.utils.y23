using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using sak.utils.FileSlicer.Common;
using sak.utils.FileSlicer.Models;

namespace sak.utils.FileSlicer
{
    public partial class SliceSvc
    {
        private static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        public IRunConfig Run_Config { get; set; }
        
        
        public SliceSvc(IRunConfig rconfig)
        {
            Run_Config = rconfig;
            log.Trace("Constructor called for SliceSvc");
        }

    }
}
