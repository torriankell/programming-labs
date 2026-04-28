using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Topic
    {
        public string StudentName { get; set; }
        public string TopicName { get; set; }
        public DateOnly Date { get; set; }

        public Topic(string studentName, string topicName, DateOnly date)
        {
            this.StudentName = studentName;
            this.TopicName = topicName;
            this.Date = date;
        }

        public static Topic? CreateObjectFromString(string rec)
        {
            string[] buffer = rec.Split(';');
            if (buffer.Length != 3)
            {
                return null;
            }

            if (!DateOnly.TryParseExact(buffer[2].Trim(), "yyyy.MM.dd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateOnly date))
            {
                return null;
            }

            return new Topic(buffer[0].Trim(), buffer[1].Trim(), date);
        }
    }
}
