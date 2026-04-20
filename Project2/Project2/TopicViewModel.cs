using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Project2
{
    public class TopicViewModel : INotifyPropertyChanged
    {
        private Topic _topic;
        public ObservableCollection<Topic> topics { get; set; }
        public Topic selectedTopic
        {
            get
            {
                return selectedTopic;
            }
            set
            {
                selectedTopic = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        public TopicViewModel(Topic topic) => topics?.Add(topic);
        
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
