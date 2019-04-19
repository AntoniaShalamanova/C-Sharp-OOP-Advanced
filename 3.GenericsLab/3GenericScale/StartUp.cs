using System;

namespace GenericScale
{
    class StartUp
    {
        static void Main(string[] args)
        {
            Scale<int> scale = new Scale<int>(3, 4);

            Console.WriteLine(scale.GetHeavier());
        }
    }
}
