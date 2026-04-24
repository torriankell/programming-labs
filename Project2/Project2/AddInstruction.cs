namespace Project2
{
    public class AddInstruction : IInstruction
    {
        string _args;

        public AddInstruction(string args) { 
            _args = args;
        }

        public void Execute(TopicViewModel tvm)
        {
            Topic t = TopicParser.BuildTopicFromStrings(_args);
            tvm.Topics.Add(t);
        }
    }
}
