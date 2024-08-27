using System.Collections.Generic;
using System.Text;

namespace Vun.UnityUtils
{
    public static partial class CollectionUtils
    {
        /// <summary>
        /// Join items in <c>collection</c> into a <c>string</c>
        /// </summary>
        public static string JoinToString<T>(this ICollection<T> collection, string separator = ", ", string start = "[", string end = "]")
        {
            var builder = new StringBuilder();
            builder.Append(start);
            var i = 0;
            
            foreach (var item in collection)
            {
                builder.Append(item);

                if (i < collection.Count - 1)
                {
                    builder.Append(separator);
                }

                i++;
            }

            builder.Append(end);
            return builder.ToString();
        }

        /// <summary>
        /// Quickly remove item at <c>index</c> by move the last item in list to <c>index</c>
        /// </summary>
        public static void FastRemoveAt<T>(this List<T> list, int index)
        {
            (list[index], list[^1]) = (list[^1], list[index]);
            list.RemoveAt(list.Count - 1);
        }
    }
}