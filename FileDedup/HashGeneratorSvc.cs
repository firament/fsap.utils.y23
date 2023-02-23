using Microsoft.Extensions.Logging;

using NLog;

using sak.utils.FileDedup.Infra;
using sak.utils.FileDedup.Models;


namespace sak.utils.FileDedup
{
    public class HashGeneratorSvc
    {
        public IRunConfig Run_Settings { get; set; }
        public ILogger<HashGeneratorSvc> log { get; set; }

        public HashGeneratorSvc(IRunConfig settings, ILogger<HashGeneratorSvc> _log)
        {
            Run_Settings = settings;
            log = (_log is null)
                ? (ILogger<HashGeneratorSvc>)NLog.LogManager.GetCurrentClassLogger(this.GetType())
                : _log;
        }

        public IDedupReport Generate()
        {
            IDedupReport report = new DedupReport()
            {
                RunDate = DateTimeOffset.Now,
                RunConfig = Run_Settings,
                Result = ResultEnum.RUNNING
            };

            IInternalReport lirLocal;

            // Test Paths
            lirLocal = TestPaths(Run_Settings);

            // prep container
            // process entries

            return report;
        }

        private IInternalReport TestPaths(IRunConfig settings)
        {
            IInternalReport ir = new InternalReport();
            ResultEnum rv = ResultEnum.RUNNING;
            int liPathsOK = 0;
            //if (Directory.Exists(p)) liPathsOK++;

            settings.Paths.ForEach((string p) =>
            {
                if (Directory.Exists(p))
                {
                    liPathsOK++;
                    ir.Entries.Add($"OK. Input path: {p}");
                }
                else
                {
                    ir.Entries.Add($"Bad input path: {p}");
                }
            });

            if (liPathsOK == 0)
            {
                rv = ResultEnum.FAIL;
            }
            else if (liPathsOK < settings.Paths.Count)
            {
                rv = ResultEnum.PARTIAL_FAIL;
            }


            if (!Directory.Exists(settings.Out_Path))
            {
                ir.Entries.Add($"Output path does not exist, attempting to create: {settings.Out_Path}");
                Directory.CreateDirectory(settings.Out_Path);
            }

            if (Directory.Exists(settings.Out_Path))
            {
                ir.Entries.Add($"OK. Output path: {settings.Out_Path}");
            }
            else
            {
                ir.Entries.Add($"Output path does not exist and cannot be created: {settings.Out_Path}");
                rv = ResultEnum.FAIL;

                //rv = rv switch
                //{
                //    ResultEnum.NOT_SET or ResultEnum.SUCCESS or ResultEnum.PARTIAL_FAIL => ResultEnum.PARTIAL_FAIL,
                //    _ => ResultEnum.FAIL,
                //};
            }

            ir.Result = (rv == ResultEnum.RUNNING) ? ResultEnum.SUCCESS : rv;
            return (ir);
        }
    }
}
