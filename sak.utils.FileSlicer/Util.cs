namespace sak.utils.FileSlicer
{
    public class FileSlicerUtils
    {
        private static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        public void Hello()
        {
            log.Trace("Hello from sak.utils.FileSlicer.Util.Hello()");
            log.Debug("Hello from sak.utils.FileSlicer.Util.Hello()");
            log.Info("Hello from sak.utils.FileSlicer.Util.Hello()");
            log.Warn("Hello from sak.utils.FileSlicer.Util.Hello()");
            log.Error("Hello from sak.utils.FileSlicer.Util.Hello()");
        }
    }
}
