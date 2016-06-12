using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap
{
    public class MaxHeap<T> : IHeap<T> where T : IComparable<T>
    {
        private List<T> _list;

        public MaxHeap()
        {
            _list = new List<T>();
        }

        public int Count { get { return _list.Count; } }

        public T Peek()
        {
            if (Count == 0)
                throw new ArgumentOutOfRangeException();

            return _list[0];
        }

        public void Push(T item)
        {
            _list.Add(item);
            BubbleUp(_list.Count - 1);
        }
 
        public T Pop()
        {
            if (this.Count == 0) 
                throw new ArgumentOutOfRangeException();

            var value = _list[0];
            _list[0] = _list[Count - 1];
            _list.RemoveAt(Count - 1);
            BubbleDown(0);

            return value;
        }

        private void BubbleDown(int index)
        {
            int firstChildIndex = index*2 + 1;
            int secondChildIndex = firstChildIndex + 1;

            int? bubbleDownIndex = null;
            if (IndexInHeap(firstChildIndex) && _list[index].CompareTo(_list[firstChildIndex]) < 0)
            {
                bubbleDownIndex = firstChildIndex;
            }

            if (IndexInHeap(secondChildIndex) 
                && _list[index].CompareTo(_list[secondChildIndex]) < 0
                && _list[secondChildIndex].CompareTo(_list[firstChildIndex]) > 0)
            {
                bubbleDownIndex = secondChildIndex;
            }

            if (bubbleDownIndex.HasValue)
            {
                Swap(index, bubbleDownIndex.Value);
                BubbleDown(bubbleDownIndex.Value);
            }
        }

        private bool IndexInHeap(int index)
        {
            return index >= 0 && index < _list.Count;
        }

        private void BubbleUp(int index)
        {
            if (index == 0)
                return;

            int parentIndex = ParentIndex(index);

            if (_list[parentIndex].CompareTo(_list[index]) < 0)
            {
                Swap(parentIndex, index);
                BubbleUp(parentIndex);
            }
        }

        private int ParentIndex(int index)
        {
            int parent = index;
            parent -= index%2 == 0 ? 2 : 1;
            return parent/2;
        }

        private void Swap(int index1, int index2)
        {
            var temp = _list[index1];
            _list[index1] = _list[index2];
            _list[index2] = temp;
        }
    }
}
