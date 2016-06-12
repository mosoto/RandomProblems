using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Heap.Test
{
    public class MaxHeapTests
    {
        private IHeap<int> ClassUnderTest { get; set; }

        public MaxHeapTests()
        {
            ClassUnderTest = new MaxHeap<int>();
        }

        public class Count : MaxHeapTests
        {
            [Fact]
            public void KeepsTrackOfNumberOfItems()
            {
                Assert.Equal(0, ClassUnderTest.Count);

                int expectedCount = 0;
                foreach (var item in Enumerable.Range(0, 10).Shuffle())
                {
                    ClassUnderTest.Push(item);
                    Assert.Equal(++expectedCount, ClassUnderTest.Count);
                }

                for (int count = expectedCount; count > 0; count--)
                {
                    ClassUnderTest.Pop();
                    Assert.Equal(--expectedCount, ClassUnderTest.Count);
                }

                Assert.Equal(0, ClassUnderTest.Count);
            }
        }

        public class Push : MaxHeapTests
        {
            [Fact]
            public void FirstItemIsPlacedOnTop()
            {
                ClassUnderTest.Push(3);
                Assert.Equal(3, ClassUnderTest.Peek());
            }

            [Fact]
            public void KeepsMaxItemOnTop1()
            {
                ClassUnderTest.Push(1);
                ClassUnderTest.Push(2);

                Assert.Equal(2, ClassUnderTest.Peek());

                ClassUnderTest.Push(1);
                Assert.Equal(2, ClassUnderTest.Peek());

                ClassUnderTest.Push(3);
                Assert.Equal(3, ClassUnderTest.Peek());
            }
        }

        public class Pop : MaxHeapTests
        {
            [Fact]
            public void WhenPopKeepsMaxItemOnTop1()
            {
                ClassUnderTest.Push(1);
                ClassUnderTest.Push(2);
                ClassUnderTest.Push(3);
                ClassUnderTest.Push(4);

                for (int count = 4; count > 0; count--)
                {
                    Assert.Equal(count, ClassUnderTest.Peek());
                    ClassUnderTest.Pop();
                }

                Assert.Equal(0, ClassUnderTest.Count);
            }

            [Fact]
            public void WhenPopKeeMaxItemOnTop2()
            {
                foreach (var item in Enumerable.Range(0, 10).Shuffle())
                {
                    ClassUnderTest.Push(item);
                }

                for (int expectedValue = 9; expectedValue >= 0; expectedValue--)
                {
                    Assert.Equal(expectedValue, ClassUnderTest.Peek());
                    ClassUnderTest.Pop();
                }
            }

            [Fact]
            public void ThrowsIfPoppingEmptyHeap()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => ClassUnderTest.Pop());
            }
        }
    }
}
