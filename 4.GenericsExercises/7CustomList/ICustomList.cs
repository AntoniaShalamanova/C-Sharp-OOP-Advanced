namespace _7CustomList
{
    public interface ICustomList<T>
    {
        int Count { get; }

        void Add(T element);

        T Remove(int index);

        bool Contains(T element);

        void Swap(int index1, int index2);

        int CountGreaterThan(T element);

        void Sort();

        T Max();

        T Min();

        T this[int number] { get; set; }
    }
}
