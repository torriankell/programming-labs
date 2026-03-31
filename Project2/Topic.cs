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
}