using System.IO;

namespace FrequencyAnalysis
{
    class FileTextHanlder
    {
        private TextHandler _tHandler;
        public string? FilePath { get; private set; }

        public FileTextHanlder(string? path)
        {
            FilePath = path;
            string content = ValidatePath();
            _tHandler = new TextHandler(content);
        }

        public Dictionary<char, double> GetFreqs() => _tHandler.GetFreqs();

        private string ValidatePath() {
            if (string.IsNullOrEmpty(FilePath))
                throw new ArgumentNullException("Path is not defined");
            else if (!File.Exists(FilePath))
                throw new ArgumentException("The file doesn't exist");
            return File.ReadAllText(FilePath);
        }

    }
}
