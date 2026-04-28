using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1w
{
    internal class Topic
    {
	public string studentName { get; set; }
        public string topicName { get; set; }
        public DateTime date { get; set; }

        public Topic(string studentName, string topicName, DateTime date) {
            this.studentName = studentName;
            this.topicName = topicName;
            this.date = date;
            Console.WriteLine("You`ve created an object of class Topic");

	Console.WriteLine($"Student`s name {this.studentName}\nTopic {this.topicName}\nDate {this.date}\nColor {this.color}");
        }
    }
}
