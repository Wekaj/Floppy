using System.Collections.Generic;

namespace Floppy.Extensions {
    /// <summary>
    /// Contains extension methods to help with <see cref="ICollection{T}"/> operations.
    /// </summary>
    internal static class ICollectionExtensions {
        #region Internal Methods

        /// <summary>
        /// Adds a range of items to a collection. This method simply enumerates over the item source and adds
        /// each item to the collection one-by-one.
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="collection">The collection to add the items to.</param>
        /// <param name="items">The item source.</param>
        internal static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items) {
            foreach (T item in items) {
                collection.Add(item);
            }
        }

        #endregion
    }
}
