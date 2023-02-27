using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using NLog;

using sak.utils.FileDedup.Models;

namespace sak.utils.FileDedup.Support

{
    internal static class Converters
    {
        //private static readonly ILogger<Converters> log = (ILogger<Converters>)NLog.LogManager.GetCurrentClassLogger();
        private static NLog.ILogger log = NLog.LogManager.GetCurrentClassLogger();
        private static JsonSerializerSettings JSS_READ = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Include,
        };
        private static JsonSerializerSettings JSS_WRITE_PRETTY = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Include,
            Formatting = Formatting.Indented,
        };
        private static JsonSerializerSettings JSS_WRITE_COMP = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Include,
            Formatting = Formatting.None,
        };

        public static IRunConfig? ConfigFromJson(string json)
        {
            RunConfig? loRC = null;
            if (string.IsNullOrWhiteSpace(json))
            {
                log.Error("Cannot parse an empty string.");
                return null;
            }
            try
            {
                loRC = JsonConvert.DeserializeObject<RunConfig>(json, JSS_READ);
            }
            catch (Exception ex)
            {

                loRC = null;
                // TODO: Log exception message somewhere
                log.Error(ex, "Deserialize failed.");
            }
            return loRC;
        }

        public static string JsonFromObject(IDedupReport<IFileEntry> ddr)
        {
            // Check for null
            try
            {
                string lsJson = JsonConvert.SerializeObject(ddr, JSS_WRITE_PRETTY);
                log.Debug("OK   Serialized object to json of length {0}", lsJson.Length);
                return lsJson;
            }
            catch (Exception ex)
            {
                log.Error(ex, "FAIL Serializing object.");
                return string.Empty;
            }
        }
    }
}
