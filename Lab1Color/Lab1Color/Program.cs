using Lab1Color;

Console.WriteLine("Enter student`s name, topic, date (YYYY.MM.DD), color:");

string[] rawData;
DateTime dt;

while (true)
{
    rawData = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
    if (rawData.Length != 4)
    {
        Console.WriteLine("Wrong params number. Try again:");
        continue;
    }

    if (DateTime.TryParseExact(rawData[2], "yyyy.MM.dd",
       System.Globalization.CultureInfo.InvariantCulture,
       System.Globalization.DateTimeStyles.None, out dt))
    {
        break;
    }
    else
    {
        Console.WriteLine("Error: Invalid date format. Please use YYYY.MM.DD format. Try again:");
    }

}

ColorTopic topic = new ColorTopic(rawData[0], rawData[1], dt, rawData[3]);

Console.WriteLine($"Student`s name {topic.studentName}\nTopic {topic.topicName}\nDate {topic.date}\nColor {topic.color}");