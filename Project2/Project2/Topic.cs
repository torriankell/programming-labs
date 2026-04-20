using System.ComponentModel;
using System.Runtime.CompilerServices;

public class Topic : INotifyPropertyChanged
{
    private string _studentName;
    private string _topicName;
    private DateOnly _date;

    public string StudentName
    {
        get { return _studentName; }
        set
        {
            _studentName = value;
            OnPropertyChanged("StudentName");
        }
    }

    public string TopicName
    {
        get { return _topicName; }
        set
        {
            _topicName = value;
            OnPropertyChanged("TopicName");
        }
    }

    public DateOnly Date 
    {
        get { return _date; }
        set
        {
            _date = value;
            OnPropertyChanged("Date");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}