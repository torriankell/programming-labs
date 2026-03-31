using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Menu
{
    internal class Topic
    {
        public string studentName { get; set; }
        public string topicName { get; set; }
        public DateTime date { get; set; }

        public Topic(string studentName, string topicName, DateTime date)
        {
            this.studentName = studentName;
            this.topicName = topicName;
            this.date = date;
            Console.WriteLine("You`ve created an object of class Topic");
        }
    }
}
