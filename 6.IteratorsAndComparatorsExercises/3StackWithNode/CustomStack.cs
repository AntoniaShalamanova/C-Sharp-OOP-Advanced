using System;
using System.Collections;
using System.Collections.Generic;

namespace _3StackWithNode
{
    public class CustomStack<T> : IEnumerable<T>
    {
        private Node<T> top;

        public CustomStack()
        {
            this.top = null;
        }

        public void Push(T element)
        {
            Node<T> newNode = new Node<T>(element);

            if (this.top == null)
            {
                this.top = newNode;
            }
            else
            {
                Node<T> tempNode = this.top;

                this.top = newNode;
                this.top.prev = tempNode;
            }
        }

        public T Pop()
        {
            if (this.top == null)
            {
                throw new InvalidOperationException("No elements");
            }

            T topElement = this.top.element;

            this.top = this.top.prev;

            return topElement;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> tempNode = this.top;

            while (tempNode != null)
            {
                yield return tempNode.element;

                tempNode = tempNode.prev;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class Node<T>
        {
            public T element { get; set; }
            public Node<T> prev { get; set; }

            public Node(T element)
            {
                this.element = element;
                this.prev = null;
            }
        }
    }
}
