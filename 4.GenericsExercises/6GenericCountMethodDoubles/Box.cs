using System;
using System.Collections.Generic;

namespace _6GenericCountMethodDoubles
{
    public class Box<T>
        where T : IComparable<T>
    {
        public int CountGreaterElemnts(List<T> elements, 
            T elementToCompare)
        {
            int count = 0;

            foreach (var item in elements)
            {
                if (item.CompareTo(elementToCompare) > 0)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
