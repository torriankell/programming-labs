using Lab1Menu;


Console.WriteLine("Enter student`s name, topic, date (YYYY.MM.DD):");

List<Topic> topicList = new List<Topic>();


while (true)
{
    DisplayMenu();
}

void DisplayMenu()
{
    string[] rawData;
    DateTime dt;

    Console.WriteLine("\nThis is menu.\n1. Add an object\n2. Display all the objects");
    char choice = Console.ReadKey().KeyChar;
    Console.ReadLine();

    if (choice == '1')
    {
        Console.WriteLine("Name, topic, date");
        rawData = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (rawData.Length != 3)
        {
            Console.WriteLine("Wrong params number. Try again:");
            return;
        }

        if (DateTime.TryParseExact(rawData[2], "yyyy.MM.dd",
           System.Globalization.CultureInfo.InvariantCulture,
           System.Globalization.DateTimeStyles.None, out dt))
        {
            topicList.Add(new Topic(rawData[0], rawData[1], dt));
            Console.WriteLine("Topic added successfully!");
        }
        else
        {
            Console.WriteLine("Error: Invalid date format. Please use YYYY.MM.DD format. Try again:");
        }
    }
    else if (choice == '2')
    {
        if (topicList != null && topicList.Count > 0) {
            foreach (var t in topicList) {
                Console.WriteLine($"Student`s name {t.studentName}\nTopic {t.topicName}\nDate {t.date}\n");
            }
        }
        else
        {
            Console.WriteLine("No available topics.");
        }
    }
        
}