using _8PetClinics.Core;

namespace _8PetClinics
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Engine engine = new Engine(new CommandInterpreter());

            engine.Run();
        }
    }
}
