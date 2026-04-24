using System.IO;

namespace Project2
{
    internal class Logger : IDisposable
    {
        private StreamWriter _writer;

        public Logger (string pathToLogFile) =>
            _writer = new StreamWriter (pathToLogFile);

        public void WriteLogLine(string line)
        {
            _writer?.WriteLine(line);
        }

        public void Dispose()
        {
            _writer?.Dispose();
        }
    }
}
