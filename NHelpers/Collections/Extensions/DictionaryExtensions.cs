﻿namespace EasySharp.NHelpers.Collections.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class DictionaryExtensions
    {
        /// <summary>
        ///     For the given <paramref name="key" /> creates an <see cref="ICollection{T}" /> if it doesn't exist and maps it to
        ///     the <paramref name="key" /> and returns the reference to its associated <see cref="ICollection{T}" />
        /// </summary>
        /// <example>
        ///     <code>
        /// foreach (Employee employee in employeesCollection) {
        ///     dictionary.MapCollectionToKey(key, () =&gt; new List&lt;Employee&gt;()).add(employee);
        /// }
        /// </code>
        ///     instead of
        ///     <code>
        /// foreach (Employee employee in employeesCollection) {
        ///     if (dictionary[key] == null)
        ///         dictionary.put(key), new List&lt;Employee&gt;());
        ///     
        ///     dictionary[key].add(employee);
        /// }
        /// </code>
        /// </example>
        /// <typeparam name="TKey">Type of key</typeparam>
        /// <typeparam name="TValue">Type of value (that is <see cref="ICollection{T}" />)</typeparam>
        /// <typeparam name="TElement">Type of elements in the <typeparamref name="TValue" /> collection</typeparam>
        /// <param name="dictionary">source dictionary</param>
        /// <param name="key">
        ///     to which will be mapped (if isn't already) a value of type <see cref="ICollection{T}" /> where T is
        ///     <typeparamref name="TElement" />
        /// </param>
        /// <param name="mappingFunc">Function that performs value (<see cref="ICollection{T}" />) mapping to the given key</param>
        /// <returns>
        ///     <typeparamref name="TValue" /> that implements <see cref="ICollection{T}" /> where T is
        ///     <typeparamref name="TElement" />
        /// </returns>
        public static TValue MapCollectionToKey<TKey, TValue, TElement>(
            this Dictionary<TKey, TValue> dictionary,
            TKey key,
            Func<ICollection<TElement>> mappingFunc)
            where TValue : ICollection<TElement>
        {
            if (mappingFunc == null)
                throw new ArgumentNullException(nameof(mappingFunc));

            if (!dictionary.ContainsKey(key))
            {
                var value = (TValue) mappingFunc();
                if (value != null)
                    dictionary.Add(key, value);
                else throw new InvalidOperationException($"{nameof(mappingFunc)} body is null");
            }
            return dictionary[key];
        }
    }
}