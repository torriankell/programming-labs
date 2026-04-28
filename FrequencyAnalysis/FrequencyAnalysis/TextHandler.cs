namespace FrequencyAnalysis
{
    class TextHandler
    {
        public string TextToHandle { get; private set; }

        public TextHandler(string text) => TextToHandle = text;

        public Dictionary<char, double> GetFreqs()
        {
            Dictionary<char, double> freqs = new Dictionary<char, double>();
            for (int i = 0; i < TextToHandle.Length; i++)
                if (!freqs.ContainsKey(TextToHandle[i]) 
                    && (FreqTables.Russian.ContainsKey(TextToHandle[i])) 
                    || FreqTables.English.ContainsKey(TextToHandle[i]))
                    freqs[TextToHandle[i]] = (double)TextToHandle.Count(ch => ch == TextToHandle[i]) / TextToHandle.Length;
            return freqs;
        }
    }
}
