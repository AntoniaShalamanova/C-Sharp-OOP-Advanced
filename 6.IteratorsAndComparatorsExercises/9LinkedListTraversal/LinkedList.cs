using System.Collections;
using System.Collections.Generic;

namespace _9LinkedListTraversal
{
    public class LinkedList<T> : IEnumerable<T>
    {
        private Node<T> Start;

        private Node<T> End;

        public int Count { get; private set; }

        public LinkedList()
        {
            this.Start = null;
            this.End = null;
            this.Count = 0;
        }

        public void Add(T element)
        {
            Node<T> newNode = new Node<T>(element);

            if (this.Start == null)
            {
                this.Start = newNode;
            }
            else
            {
                if (this.Start.Next == null)
                {
                    newNode.Prev = this.Start;
                    this.Start.Next = newNode;
                    this.End = newNode;
                }
                else
                {
                    Node<T> tempNode = this.End;

                    tempNode.Next = newNode;
                    this.End = newNode;
                    this.End.Prev = tempNode;
                }
            }

            this.Count++;
        }

        public void Remove(T element)
        {
            Node<T> currentNode = this.Start;

            while (currentNode != null)
            {
                if (currentNode.Element.Equals(element))
                {
                    Node<T> prev = currentNode.Prev;
                    Node<T> next = currentNode.Next;

                    if (prev == null)
                    {
                        this.Start = next;
                        next.Prev = prev;
                    }

                    if (next == null)
                    {
                        this.End = prev;
                        prev.Next = next;
                    }

                    if (prev != null && next != null)
                    {
                        prev.Next = next;
                        next.Prev = prev;
                    }

                    currentNode = null;

                    this.Count--;

                    break;
                }

                currentNode = currentNode.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> currentNode = this.Start;

            while (currentNode != null)
            {
                yield return currentNode.Element;

                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public class Node<T>
        {
            public Node(T element)
            {
                this.Prev = null;
                this.Element = element;
                this.Next = null;
            }

            public Node<T> Prev { get; set; }

            public T Element { get; set; }

            public Node<T> Next { get; set; }
        }
    }
}
