using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Home_12
{
    class Task_1
    {
        class Queue<T> : IEnumerable<T>
        {
            class Node
            {
                public T data;
                public Node next = null;
            }
            Node first;
            Node last;
            uint size;
            public uint Size
            {
                get => size;
                private set => size = value;
            }
            public IEnumerator<T> GetEnumerator()
            {
                Node tmp = first;
                while (tmp != null)
                {
                    yield return tmp.data;
                    tmp = tmp.next;
                }
                yield break;
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                Node tmp = first;
                while (tmp != null)
                {
                    yield return tmp.data;
                    tmp = tmp.next;
                }
                yield break;
            }
            public void Add(T elem)
            {
                Node tmp = new Node { data = elem };
                if (first == null)
                {
                    first = tmp;
                    last = tmp;
                }
                else
                {
                    last.next = tmp;
                    last = tmp;
                }
                ++Size;
            }
            public T Extract()
            {
                if (first == null)
                {
                    throw new InvalidOperationException("Queue empty");
                }
                T tmp = first.data;
                first = first.next;
                --Size;
                return tmp;
            }
            public T Top()
            {
                return first.data;
            }
        }
        static public void main()
        {
            Queue<int> queue = new Queue<int>();

            for (int i = 0; i < 10; i++)
            {
                queue.Add(i);
            }

            Console.WriteLine(queue.Extract() + " Extract");
            Console.WriteLine(queue.Extract() + " Extract");
            Console.WriteLine(queue.Extract() + " Extract");

            Console.WriteLine(queue.Top() + " Top");

            Console.WriteLine("\n---foreach---");
            foreach (object item in queue)
            {
                Console.WriteLine(item);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
           // Task_1.main();
            Task_2.main();//завдвня з картами у ньому використовував LinkedList

        }
    }
}
