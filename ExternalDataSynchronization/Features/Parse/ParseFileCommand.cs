using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataSynchronization.Features.Parse
{
    public class ParseFileCommand
    {
        public string filePath { get; }
        public ParseFileCommand(string filePath)
        {
            this.filePath = filePath;
        }
    }
}
