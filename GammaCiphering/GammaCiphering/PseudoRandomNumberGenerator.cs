namespace GammaCiphering
{
    internal class PseudoRandomNumberGenerator
    {
        private int _a, _c, _m, _seed, _initialSeed;
        public PseudoRandomNumberGenerator(int a, int c, int m, int seed) {
            _a = a;
            _c = c;
            _m = m;
            _seed = seed;
            _initialSeed = seed;
        }

        public int GetRandomNumber() => _seed = (_a * _seed + _c) % _m;

        public void Reset() => _seed = _initialSeed;

        public void TestGenerator(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(GetRandomNumber() + "\t");
                if ((i + 1) % (n / 5) == 0) Console.WriteLine();
            }
        }
    }
}
