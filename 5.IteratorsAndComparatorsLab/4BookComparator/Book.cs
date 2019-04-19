using System.Collections.Generic;
using System.Linq;

namespace IteratorsAndComparators
{
    public class Book
    {
        public string Title { get; set; }

        public int Year { get; set; }

        public IReadOnlyCollection<string> Authors { get; private set; }

        public Book(string title, int year, params string[] authors)
        {
            this.Title = title;
            this.Year = year;
            this.Authors = authors.ToList();
        }

        public override string ToString()
        {
            return $"{this.Title} - {this.Year}";
        }
    }
}
