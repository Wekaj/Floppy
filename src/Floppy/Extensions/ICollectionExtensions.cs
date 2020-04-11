using System.Collections.Generic;

namespace Floppy.Extensions {
    internal static class ICollectionExtensions {
        #region Internal Methods

        internal static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items) {
            foreach (T item in items) {
                collection.Add(item);
            }
        }

        #endregion
    }
}
