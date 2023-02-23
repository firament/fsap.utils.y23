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
