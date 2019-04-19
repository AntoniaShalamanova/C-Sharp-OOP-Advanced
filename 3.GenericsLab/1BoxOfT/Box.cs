using System.Collections.Generic;
using System.Linq;

namespace BoxOfT
{
    public class Box<T>
    {
        IList<T> items;

        public int Count => this.items.Count;

        public Box()
        {
            this.items = new List<T>();
        }

        public void Add(T item)
        {
            this.items.Add(item);
        }

        public T Remove()
        {
            T item = this.items.Last();

            this.items.RemoveAt(this.items.Count - 1);

            return item;
        }
    }
}
