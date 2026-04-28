using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace WpfApp2
{
    internal class Code39Alg
    {
        private int _barcodeHeight = 100;
        private int _narrowWidth = 2;
        private int _wideWidth = 5;
        private int _emptyZoneWidth = 40;

        public string Word { get; set; }

        public Dictionary<char, string> Code39Alphabet = new() {
            {'*', "NwNnWnWnN"}, {'-', "NwNnNnWnW"}, {'.', "WwNnNnWnN"}, {'$', "NwNwNwNnN"},
            {'/', "NwNwNnNwN"}, {'+', "NwNnNwNwN"}, {'%', "NnNwNwNwN"}, {' ', "NwWnNnWnN"},
            {'0', "NnNwWnWnN"}, {'1', "WnNwNnNnW"}, {'2', "NnWwNnNnW"}, {'3', "WnWwNnNnN"},
            {'4', "NnNwWnNnW"}, {'5', "WnNwWnNnN"}, {'6', "NnWwWnNnN"}, {'7', "NnNwNnWnW"},
            {'8', "WnNwNnWnN"}, {'9', "NnWwNnWnN"},
            {'A', "WnNnNwNnW"}, {'B', "NnWnNwNnW"}, {'C', "WnWnNwNnN"}, {'D', "NnNnWwNnW"},
            {'E', "WnNnWwNnN"}, {'F', "NnWnWwNnN"}, {'G', "NnNnNwWnW"}, {'H', "WnNnNwWnN"},
            {'I', "NnWnNwWnN"}, {'J', "NnNnWwWnN"}, {'K', "WnNnNnNwW"}, {'L', "NnWnNnNwW"},
            {'M', "WnWnNnNwN"}, {'N', "NnNnWnNwW"}, {'O', "WnNnWnNwN"}, {'P', "NnWnWnNwN"},
            {'Q', "NnNnNnWwW"}, {'R', "WnNnNnWwN"}, {'S', "NnWnNnWwN"}, {'T', "NnNnWnWwN"},
            {'U', "WwNnNnNnW"}, {'V', "NwWnNnNnW"}, {'W', "WwWnNnNnN"}, {'X', "NwNnWnNnW"},
            {'Y', "WwNnWnNnN"}, {'Z', "NwWnWnNnN"}
        };

        public Code39Alg(string word)
        {
            Word = word;
        }

        public void GetCode(Canvas barcodeCanvas)
        {
            if (string.IsNullOrEmpty(Word))
                throw new ArgumentException("Строка для штрихкода не может быть пустой");

            barcodeCanvas.Children.Clear();

            List<string> patterns = ConvertToPatterns(Word);
            int currentX = _emptyZoneWidth;

            foreach (string pattern in patterns)
            {
                for (int j = 0; j < pattern.Length; j++)
                {
                    char element = pattern[j];
                    bool isBar = (j % 2 == 0);
                    int width = (element == 'W' || element == 'w') ? _wideWidth : _narrowWidth;

                    if (isBar)
                    {
                        Rectangle rect = new Rectangle
                        {
                            Width = width,
                            Height = _barcodeHeight,
                            Fill = Brushes.Black,
                            SnapsToDevicePixels = true
                        };
                        Canvas.SetLeft(rect, currentX);
                        Canvas.SetTop(rect, 0);
                        barcodeCanvas.Children.Add(rect);
                    }

                    currentX += width;
                }
            }

            barcodeCanvas.Width = currentX + _emptyZoneWidth;
            barcodeCanvas.Height = _barcodeHeight;
        }

        private List<string> ConvertToPatterns(string word)
        {
            List<string> patterns = new List<string>();

            string upperWord = word.ToUpper();

            foreach (char c in upperWord)
                if (!Code39Alphabet.ContainsKey(c))
                    throw new ArgumentException($"Символ '{c}' не поддерживается в Code39");

            patterns.Add(Code39Alphabet['*']);

            foreach (char c in upperWord)
            {
                patterns.Add("nn");
                patterns.Add(Code39Alphabet[c]);
            }

            patterns.Add("nn");
            patterns.Add(Code39Alphabet['*']);

            return patterns;
        }
    }
}