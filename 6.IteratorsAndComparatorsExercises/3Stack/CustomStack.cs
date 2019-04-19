using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace _3Stack
{
    public class CustomStack<T> : IEnumerable<T>
    {
        private T[] elements;
        public int Count { get; private set; }

        public CustomStack()
        {
            this.elements = new T[10];
            this.Count = 0;
        }

        public void Push(T element)
        {
            if (this.Count == this.elements.Length)
            {
                ExtendArray();
            }

            this.elements[this.Count] = element;
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("No elements");
            }

            T lastElement = this.elements[this.Count - 1];
            this.elements[this.Count - 1] = default(T);
            this.Count--;

            return lastElement;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                yield return this.elements[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void ExtendArray()
        {
            T[] temp = new T[this.elements.Length * 2];

            for (int i = 0; i < this.Count; i++)
            {
                temp[i] = this.elements[i];
            }

            this.elements = temp;
        }
    }
}
