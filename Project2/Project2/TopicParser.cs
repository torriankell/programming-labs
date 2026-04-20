using System.Windows.Controls;

namespace Project2
{
    public class TopicParser
    {
        private const char SEPARATOR = ';';

        public static Topic? BuildTopicFromStrings(string topic)
        {
            if (string.IsNullOrEmpty(topic))
                throw new ArgumentException("Строка пуста");

            string[] buffer = topic.Split(SEPARATOR);
            if (buffer.Length != 3)
                throw new FormatException($"Ожидалось 3 поля, получено {buffer.Length}");

            if (string.IsNullOrWhiteSpace(buffer[0]))
                throw new ArgumentException("Имя студента пусто");

            if (string.IsNullOrWhiteSpace(buffer[1]))
                throw new ArgumentException("Название темы пусто");

            if (!DateOnly.TryParseExact(buffer[2].Trim(), "yyyy.MM.dd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateOnly date))
                throw new FormatException($"Неверный формат даты: {buffer[2]}");

            string stuName = buffer[0].Trim();
            string topicName = buffer[1].Trim();

            return new Topic(stuName, topicName, date);
        }
    }
}
