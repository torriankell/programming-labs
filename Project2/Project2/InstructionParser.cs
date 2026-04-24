using System.IO;
using System.Text;

namespace Project2
{
    public class InstructionParser
    {
        public List<IInstruction> ParseCommands(string pathToInstFile)
        {
            List<IInstruction> insts = new List<IInstruction>();
            string line;

            using (StreamReader reader = new StreamReader(pathToInstFile, Encoding.UTF8))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    line = line.Trim();
                    string[] tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    string keyword = tokens[0].ToUpper();
                    string args = string.Join(" ", tokens.Skip(1));

                    switch (keyword)
                    {
                        case "ADD":
                            insts.Add(new AddInstruction(args));
                            break;
                        case "REM":
                            insts.Add(new RemInstruction(args));
                            break;
                        case "SAVE":
                            insts.Add(new SaveInstruction(args));
                            break;
                        default:
                            throw new ArgumentException($"Неизвестная команда: {keyword}");
                    }
                }
            }

            return insts;
        }
    }
}
