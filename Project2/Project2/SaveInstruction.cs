using System.IO;
using System.Windows;

namespace Project2
{
    internal class SaveInstruction : IInstruction
    {
        string _args;
        public SaveInstruction(string args)
        {
            _args = args;
        }
        public void Execute(TopicViewModel tvm)
        {
            string[] tokens = _args.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length != 1)
                throw new ArgumentException("Неверное количество параметров");

            using (StreamWriter sw = new StreamWriter(tokens[0], false))
            {
                foreach (Topic t in tvm.Topics)
                    sw.Write($"{t.StudentName};{t.TopicName};{t.Date:yyyy.MM.dd}\n");
            }
            MessageBox.Show($"Файл сохранён: {Path.GetFullPath(tokens[0])}");
        }
    }
}
