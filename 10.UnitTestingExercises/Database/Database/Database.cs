using System;
using System.Linq;

public class Database
{
    private const int Capacity = 16;
    private int index;
    private int[] data;

    public Database()
    {
        this.data = new int[Capacity];
        this.index = -1;
    }

    public Database(int[] numbers)
        : this()
    {
        if (numbers.Length > 16)
        {
            throw new InvalidOperationException();
        }

        for (int i = 0; i < numbers.Length; i++)
        {
            this.data[i] = numbers[i];
        }

        this.index = numbers.Length - 1;
    }

    public void Add(int number)
    {
        if (this.index == Capacity - 1)
        {
            throw new InvalidOperationException();
        }

        this.data[++this.index] = number;
    }

    public void Remove()
    {
        if (this.index == -1)
        {
            throw new InvalidOperationException();
        }

        this.data[this.index--] = 0;
    }

    public int[] Fetch()
    {
        return this.data.Take(this.index + 1).ToArray();
    }
}
