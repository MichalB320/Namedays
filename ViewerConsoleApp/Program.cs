namespace ViewerConsoleApp;

public class Program
{
    static void Main(string[] args)
    {
        
        IS isS = new IS(args[0]);
        isS.run();
    }
}