using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap.Test
{
    public static class ExtensionMethods
    {
        public static Random _random = new Random();

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
        {
            return list.OrderBy(item => _random.Next());
        }
    }
}
