using System;
using System.Collections;
using System.Collections.Generic;

namespace _7CustomList
{
    public class CustomList<T> : ICustomList<T>, IEnumerable<T>
        where T : IComparable<T>
    {
        private const int InitialCapacity = 4;

        private T[] array;

        public int Count { get; private set; }

        public CustomList()
        {
            this.array = new T[InitialCapacity];
            this.Count = 0;
        }

        public void Add(T element)
        {
            if (this.Count == this.array.Length)
            {
                this.Resize();
            }

            this.array[this.Count] = element;
            this.Count++;
        }

        public bool Contains(T element)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.array[i].Equals(element))
                {
                    return true;
                }
            }

            return false;
        }

        public int CountGreaterThan(T element)
        {
            int count = 0;

            for (int i = 0; i < this.Count; i++)
            {
                if (this.array[i].CompareTo(element) > 0)
                {
                    count++;
                }
            }

            return count;
        }

        public T Max()
        {
            T maxValue = this.array[0];

            for (int i = 1; i < this.Count; i++)
            {
                if (this.array[i].CompareTo(maxValue) > 0)
                {
                    maxValue = this.array[i];
                }
            }

            return maxValue;
        }

        public T Min()
        {
            T minValue = this.array[0];

            for (int i = 1; i < this.Count; i++)
            {
                if (this.array[i].CompareTo(minValue) < 0)
                {
                    minValue = this.array[i];
                }
            }

            return minValue;
        }

        public T Remove(int index)
        {
            T element = this.array[index];

            for (int i = index; i < this.Count; i++)
            {
                if (i == this.Count - 1)
                {
                    this.array[i] = default(T);
                    this.Count--;

                    break;
                }

                this.array[i] = this.array[i + 1];
            }

            return element;
        }

        public void Swap(int index1, int index2)
        {
            T temp = this.array[index1];

            this.array[index1] = this.array[index2];
            this.array[index2] = temp;
        }

        public void Sort()
        {
            for (int i = 0; i < this.Count; i++)
            {
                for (int j = i + 1; j < this.Count; j++)
                {
                    if (this.array[i].CompareTo(this.array[j]) > 0)
                    {
                        T temp = this.array[i];

                        this.array[i] = this.array[j];
                        this.array[j] = temp;
                    }
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public T this[int number]
        {
            get
            {
                // This is invoked when accessing Layout with the [ ].
                if (number >= 0 && number < this.Count)
                {
                    // Bounds were in range, so return the stored value.
                    return this.array[number];
                }
                // Return an error string.
                throw new IndexOutOfRangeException();
            }
            set
            {
                // This is invoked when assigning to Layout with the [ ].
                if (number >= 0 && number < this.Count)
                {
                    // Assign to this element slot in the internal array.
                    this.array[number] = value;
                }

                throw new IndexOutOfRangeException();
            }
        }

        private void Resize()
        {
            T[] tempArray = new T[this.array.Length * 2];

            for (int i = 0; i < this.array.Length; i++)
            {
                tempArray[i] = this.array[i];
            }

            this.array = tempArray;
        }
    }
}
