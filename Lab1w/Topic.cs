using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1w
{
    internal class Topic
    {
        string studentName;
        string topicName;
        DateTime date;

        public Topic(string studentName, string topicName, DateTime date) {
            this.studentName = studentName;
            this.topicName = topicName;
            this.date = date;
            Console.WriteLine("You`ve created an object of class Topic");
        }
    }
}
