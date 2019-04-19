using _7CustomList.Core;

namespace _7CustomList
{
    class StartUp
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine(new CommandInterpreter());
            engine.Run();
        }
    }
}
