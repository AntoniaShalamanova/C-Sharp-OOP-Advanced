using System.Collections.Generic;

namespace _6StrategyPattern
{
    class NameComparator : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            string firstName = x.Name.ToLower();
            string secondName = y.Name.ToLower();

            int result = firstName.Length.CompareTo(secondName.Length);

            if (result == 0)
            {
                result = firstName.CompareTo(secondName);
            }

            return result;
        }
    }
}
