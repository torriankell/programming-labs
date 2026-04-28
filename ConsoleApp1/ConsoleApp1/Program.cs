using static System.GC;

Func();

void Func()
{
    {
        Test t = new Test("For garbage collecting");
        Console.WriteLine($"Object gen               \t {GC.GetGeneration(t)}\nAmount memory allocated \t {GC.GetTotalMemory(true)}");
        GC.Collect(0, GCCollectionMode.Forced);
        GC.WaitForPendingFinalizers();
    }
    Console.WriteLine(t.ToString());
}

public class Test
{
    string name;
    public Test(string n) {
        name = n;
    }
}