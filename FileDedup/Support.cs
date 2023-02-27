/*
 * Generated with CLR ver. 4.0.30319.42000
 * Copyright 2023 sak
 * 
 * Support.cs
 * Author      : sak
 * Contributors: 
 *  (one entry for each person that contributes to code in this file)
 *  <contributor name 1>
 *  
 * Description : 
 *  Brief description of this file's code intent.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using sak.utils.FileDedup.Models;
using sak.utils.FileDedup.Infra;

namespace sak.utils.FileDedup.Support
{
    public static class RunConfigSupport
    {
        private readonly static string DSC = Path.DirectorySeparatorChar.ToString();
        private static JsonSerializerSettings js_out = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
        };

        public static string Sample01_Json()
        {
            string lsData;
            lsData = JsonConvert.SerializeObject(Sample01(), js_out);
            return lsData;
        }
        public static IRunConfig Sample01()
        {
            DateTimeOffset dt = DateTimeOffset.Now;
            IRunConfig config = new RunConfig()
            {
                DateUpdated = dt,
                Paths = new List<string>()
                {
                    "E:/Decernis/UK9-Pulls/StripMasters"           // small number for debug.
                    //"E:/Decernis/UK9-Pulls/extract_all_210823-0821" // large number of files
                },
                Out_Path = Path.Combine(
                    Environment.CurrentDirectory,
                    $"..{DSC}..{DSC}..{DSC}",
                    DDConstants.GC_OUTPUT_PATH_SEGMENT,
                    dt.ToString(DDConstants.GC_OUTPUT_PATH_PATTERN)
                    ),
                UseHash = HashTypeEnum.SHA256,
            };

            return config;
        }
    }
}
