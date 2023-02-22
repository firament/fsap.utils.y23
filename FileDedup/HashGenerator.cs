using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sak.utils.FileDedup.Infra;
using sak.utils.FileDedup.Models;

namespace sak.utils.FileDedup
{
    public class HashGenerator
    {
        public IRunConfig Run_Settings { get; set; }
        public HashGenerator(IRunConfig settings)
        {
            Run_Settings= settings;
        }


    }
}
