using System;

namespace _10Tuple
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] firstInput = Console.ReadLine()
               .Split();

            string fullName = firstInput[0] + " " + firstInput[1];
            string address = firstInput[2];
            string town = firstInput[3];

            string[] secondInput = Console.ReadLine()
                    .Split();

            string drinkerName = secondInput[0];
            int beersCount = int.Parse(secondInput[1]);
            bool isDrunk = secondInput[2] == "drunk" ? true : false;

            string[] thirdInput = Console.ReadLine()
                    .Split();

            string personName = thirdInput[0];
            double balance = double.Parse(thirdInput[1]);
            string bankName = thirdInput[2];

            var firstTuple = new CustomTuple<string, string, string>(fullName, address, town);
            var secondTuple = new CustomTuple<string, int, bool>(drinkerName, beersCount, isDrunk);
            var thirdTuple = new CustomTuple<string, double, string>(personName, balance, bankName);

            Console.WriteLine(firstTuple);
            Console.WriteLine(secondTuple);
            Console.WriteLine(thirdTuple);
        }
    }
}
