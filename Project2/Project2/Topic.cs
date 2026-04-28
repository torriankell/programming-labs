using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Project2;

public class Topic : INotifyPropertyChanged
{
    private string _studentName;
    private string _topicName;
    private DateOnly _date;

    private string _teacherName;
    public string TeacherName
    {
        get => _teacherName;
        set { _teacherName = value; OnPropertyChanged(); }
    }

    public string StudentName
    {
        get { return _studentName; }
        set
        {
            _studentName = value;
            OnPropertyChanged();
        }
    }

    public string TopicName
    {
        get { return _topicName; }
        set
        {
            _topicName = value;
            OnPropertyChanged();
        }
    }

    public DateOnly Date 
    {
        get { return _date; }
        set
        {
            _date = value;
            OnPropertyChanged();
        }
    }

    public Topic()
    {

    }
    public Topic(string studentName, string topicName, DateOnly date, string teacherName)
    {
        StudentName = studentName;
        TopicName = topicName;
        Date = date;
        TeacherName = teacherName;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}