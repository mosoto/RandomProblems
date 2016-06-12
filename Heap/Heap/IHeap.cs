using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap
{
    public interface IHeap<T> where T : IComparable<T>
    {
        int Count { get; }

        void Push(T item);

        T Peek();

        T Pop();
    }
}
