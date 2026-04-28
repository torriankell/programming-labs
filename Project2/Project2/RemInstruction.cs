namespace Project2
{
    public class RemInstruction : IInstruction
    {
        private string _args;

        public RemInstruction(string args)
        {
            _args = args;
        }

        public void Execute(TopicViewModel tvm)
        {
            string[] tokens = _args.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length < 3)
                throw new ArgumentException("Недостаточно аргументов для REM");

            string field = tokens[0];
            string op = tokens[1];
            string value = string.Join(" ", tokens.Skip(2));

            var toRemove = tvm.Topics.Where(t => Compare(t, field, op, value)).ToList();

            foreach (var topic in toRemove)
                tvm.Topics.Remove(topic);
        }

        private bool Compare(Topic topic, string field, string op, string val)
        {
            object fieldValue = field switch
            {
                "StudentName" => topic.StudentName,
                "TopicName" => topic.TopicName,
                "Date" => topic.Date,
                "TeacherName" => topic.TeacherName,
                _ => throw new ArgumentException($"Неизвестное поле: {field}")
            };

            object parsedValue = field switch
            {
                "StudentName" => val,
                "TopicName" => val,
                "Date" => DateOnly.Parse(val),
                "TeacherName" => val,
                _ => null
            };

            return op switch
            {
                "==" => fieldValue.Equals(parsedValue),
                "!=" => !fieldValue.Equals(parsedValue),
                "<" => Comparer<object>.Default.Compare(fieldValue, parsedValue) < 0,
                "<=" => Comparer<object>.Default.Compare(fieldValue, parsedValue) <= 0,
                ">" => Comparer<object>.Default.Compare(fieldValue, parsedValue) > 0,
                ">=" => Comparer<object>.Default.Compare(fieldValue, parsedValue) >= 0,
                _ => throw new ArgumentException($"Неизвестный оператор: {op}")
            };
        }
    }
}