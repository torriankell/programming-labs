using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Color
{
    internal class ColorTopic
    {
        public string studentName { get; set; }
        public string topicName { get; set; }
        public string color { get; set; }
        public DateTime date { get; set; }

        public ColorTopic(string studentName, string topicName, DateTime date, string color)
        {
            this.studentName = studentName;
            this.topicName = topicName;
            this.date = date;
            this.color = color;
            Console.WriteLine("You`ve created an object of class Topic");
        }
    }
}
