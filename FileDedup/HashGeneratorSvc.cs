using System.Security.Cryptography;
using System.Text;

//using Microsoft.Extensions.Logging;

using NLog;

using sak.utils.FileDedup.Infra;
using sak.utils.FileDedup.Models;
using sak.utils.FileDedup.Support;


namespace sak.utils.FileDedup
{
    public class HashGeneratorSvc : IDisposable
    {
        public IRunConfig Run_Settings { get; set; }
        private static NLog.ILogger log = NLog.LogManager.GetCurrentClassLogger();

        //public HashGeneratorSvc(string settings, ILogger<HashGeneratorSvc> _log)
        public HashGeneratorSvc(string settings)
        {
            //log = (_log is null)
            //    ? (ILogger<HashGeneratorSvc>)NLog.LogManager.GetCurrentClassLogger(this.GetType())
            //    : _log;

            IRunConfig? loRC = Converters.ConfigFromJson(settings);
            Run_Settings = (loRC is not null)
                ? loRC
                : throw new NullReferenceException();
        }

        //public HashGeneratorSvc(IRunConfig settings, ILogger<HashGeneratorSvc> _log)
        public HashGeneratorSvc(IRunConfig settings)
        {
            Run_Settings = settings;
            //log = (_log is null)
            //    ? (ILogger<HashGeneratorSvc>)NLog.LogManager.GetCurrentClassLogger(this.GetType())
            //    : _log;
        }

        public IDedupReport<IFileEntry> Generate()
        {
            IDedupReport<IFileEntry> report = new DedupReport<IFileEntry>()
            {
                RunDate = DateTimeOffset.Now,
                RunConfig = Run_Settings,
                Result = ResultEnum.RUNNING
            };

            IInternalReport lirLocal;

            // Test Paths
            lirLocal = TestPaths(Run_Settings);
            report.Entries.AddRange(lirLocal.Entries); // For debug only, remove before checkin!
            switch (lirLocal.Result)
            {
                case ResultEnum.SUCCESS:
                    log.Info("OK   All input Paths");
                    break;
                case ResultEnum.PARTIAL_FAIL:
                    log.Warn("BAD  One or more input path does not exist, or is not accessible");
                    break;
                case ResultEnum.NOT_SET:
                case ResultEnum.RUNNING:
                case ResultEnum.FAIL:
                default:
                    log.Fatal("FAIL All input Paths and/or Output path do not exist or cannot be accessed");
                    log.Fatal("Abandoning process now.");
                    report.Result = ResultEnum.FAIL;
                    report.Message = "Path test failed. see logs.";
                    report.Entries.AddRange(lirLocal.Entries);
                    return report;
            }

            // prep container
            // process entries
            InternalPayload<IFileEntry> loPL;
            log.Debug("Processing all input folders");
            foreach (string vsFolder in Run_Settings.Paths)
            {
                log.Info("Processing {0}", vsFolder);
                if (string.IsNullOrEmpty(vsFolder))
                {
                    log.Warn("Path {0} is invalid.", vsFolder);
                    log.Info("Skipping folder");
                    continue; // TODO: Not skipping rest of lines.
                }
                try
                {
                    loPL = ProcessFolder(vsFolder);
                    log.Info("Hashed {0} fries. In Path {1}", loPL.Payload?.Count, vsFolder);
                    report.Payload.AddRange(loPL.Payload);
                    report.Entries.AddRange(loPL.Entries);
                }
                catch (Exception ex)
                {
                    log.Fatal(ex, "Hash error, see log entries.");
                    report.Result = ResultEnum.FAIL;
                    report.Message = ex.Message;
                    //throw;
                }
            }

            if (report.Result == ResultEnum.RUNNING)
            {
                report.Result = ResultEnum.SUCCESS;
                report.Message = "OK";
            }

            string lsOutJson = Converters.JsonFromObject(report);
            string lsOutFile = Path.Combine(
                Run_Settings.Out_Path,
                $"{DateTimeOffset.Now.ToString("OutRun_yyMMdd_HHmm.j\\son")}"
                );
            //log.Debug("JSON: {0}", lsOutJson);
            if (string.IsNullOrWhiteSpace(lsOutJson))
            {
                log.Warn("Output cannot be written to file. see logs.");
                report.Result = ResultEnum.FAIL;
            }
            else
            {
                log.Info("Writing output to file {0}", lsOutFile);
                File.WriteAllText(lsOutFile, lsOutJson, Encoding.UTF8);
            }

            return report;
        }

        private InternalPayload<IFileEntry> ProcessFolder(string path)
        {
            InternalPayload<IFileEntry> loPL = new InternalPayload<IFileEntry>() { Result = ResultEnum.RUNNING };
            //IFileEntry loFE = null;
            byte[] lyaHashData;
            string lsHash;
            FileStream lofsInput;
            DateTimeOffset ltBegin, ltFinis;

            if (!Directory.Exists(path))
            {
                log.Info("Bad Path: {0}", path);
                log.Error("Skipping Bad Path: {0}", path);
                loPL.Result = ResultEnum.FAIL; return loPL;
            }

            // TODO: use hasher type from settings
            // HashAlgorithm
            // Refactor the using blocks for better try-catch
            log.Warn("Using hardcoded hasher, 'MD5'. Consider parameterising hasher type.");
            using (MD5 hasher = MD5.Create())
            {
                foreach (string vF in Directory.GetFiles(path))
                {
                    log.Info("Hashing file {0}", vF);
                    using (lofsInput = File.OpenRead(vF))
                    {
                        ltBegin = DateTimeOffset.Now;
                        lyaHashData = hasher.ComputeHash(lofsInput);
                        lsHash = Convert.ToHexString(lyaHashData);
                        ltFinis = DateTimeOffset.Now;
                        loPL.Payload.Add(new FileEntry()
                        {
                            Hash_Type = HashTypeEnum.MD5,
                            Hash_Time = ltFinis - ltBegin,
                            Hash_Value = lsHash,
                            Name = Path.GetFileName(vF),
                            Status = RecordStatusEnum.NEW,
                            ADD_ON = ltBegin,
                            EDIT_ON = ltBegin
                        });
                        lofsInput.Close();
                        lofsInput.Dispose();
                    }
                    log.Info("Hashed in {0} milliseconds.", (ltFinis - ltBegin).TotalMilliseconds);
                    // TODO: format the number
                }
            }
            loPL.Result = (loPL.Result == ResultEnum.RUNNING)
                ? ResultEnum.SUCCESS
                : loPL.Result
                ;
            return loPL;
        }



        private IInternalReport TestPaths(IRunConfig settings)
        {
            IInternalReport ir = new InternalReport() { Result = ResultEnum.RUNNING };
            ResultEnum rv = ResultEnum.RUNNING;
            int liPathsOK = 0;
            //bool lbOK = false;
            //if (Directory.Exists(p)) liPathsOK++;

            log.Debug("Testing input paths.");
            settings.Paths.ForEach((string p) =>
            {
                if (Directory.Exists(p))
                {
                    liPathsOK++;
                    ir.Entries.Add($"OK. Input path: {p}");
                    log.Info("OK   In Path {0}", p);
                }
                else
                {
                    ir.Entries.Add($"Bad input path: {p}");
                    log.Error("FAIL In Path {0}", p);
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


            log.Debug("Testing output path.");
            if (!Directory.Exists(settings.Out_Path))
            {
                ir.Entries.Add($"Output path does not exist, attempting to create: {settings.Out_Path}");
                log.Warn("BAD  Out Path {0}", settings.Out_Path);
                log.Info("Does not exist, attempting to create.");
                Directory.CreateDirectory(settings.Out_Path);
            }

            if (Directory.Exists(settings.Out_Path))
            {
                log.Info("OK   Out Path {0}", settings.Out_Path);
                ir.Entries.Add($"OK. Output path: {settings.Out_Path}");
            }
            else
            {
                log.Fatal("FAIL Create Path {0}", settings.Out_Path);
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

        public void Dispose() => log.Info("Disposed: sak.utils.FileDedup.HashGeneratorSvc");
    }
}
