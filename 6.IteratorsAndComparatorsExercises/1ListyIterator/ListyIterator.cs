using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _1ListyIterator
{
    public class ListyIterator<T> : IEnumerable<T>
    {
        private readonly List<T> elements;
        private int internalIndex;

        public ListyIterator(T[] elements)
        {
            this.internalIndex = 0;
            this.elements = elements.ToList();
        }

        public T Print()
        {
            if (this.elements.Count == 0)
            {
                throw new InvalidOperationException("Invalid Operation!");
            }

            return this.elements[internalIndex];
        }

        public string PrintAll()
        {
            return string.Join(" ", this.elements);
        }

        public bool Move()
        {
            if (this.HasNext())
            {
                this.internalIndex++;

                return true;
            }

            return false;
        }

        public bool HasNext()
        {
            return this.internalIndex < this.elements.Count - 1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.elements.Count; i++)
            {
                yield return this.elements[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
