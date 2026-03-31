using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    internal class Logger : IDisposable
    {
        private StreamWriter _writer;
        public int Counter { get; set; } = 0;

        public Logger (string pathToLogFile) =>
            _writer = new StreamWriter (pathToLogFile);

        public void WriteLogLine(string line)
        {
            _writer?.WriteLine(line);
            Counter++;
        }

        public void Dispose()
        {
            _writer?.Dispose();
        }
    }
}
