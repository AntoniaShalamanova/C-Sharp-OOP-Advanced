﻿using System;

namespace _5ComparingObjects
{
    public class Person : IComparable<Person>
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Town { get; set; }

        public Person(string name, string age, string town)
        {
            this.Name = name;
            this.Age = age;
            this.Town = town;
        }

        public int CompareTo(Person other)
        {
            int result = this.Name.CompareTo(other.Name);

            if (result == 0)
            {
                result = this.Age.CompareTo(other.Age);
            }

            if (result == 0)
            {
                result = this.Town.CompareTo(other.Town);
            }

            return result;
        }
    }
}
