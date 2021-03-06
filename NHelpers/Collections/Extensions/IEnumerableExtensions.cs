﻿namespace EasySharp.NHelpers.Collections.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    public static class IEnumerableExtensions
    {
        /// <summary>
        ///     Chunks a collection into a collection of collections containing elements of type <typeparamref name="TElement" />.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="source"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static IEnumerable<IEnumerable<TElement>> ChunkBy<TElement>(this IEnumerable<TElement> source,
            int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value));
        }

        //public static IEnumerable<List<TElement>> ChunkBy<TElement>(this List<TElement> source,
        //    int chunkSize)
        //{
        //    for (int i = 0; i < source.Count; i += chunkSize)
        //    {
        //        yield return source.GetRange(i, Math.Min(chunkSize, source.Count - i));
        //    }
        //}

        ///<summary>Finds the index of the first item matching an expression in an enumerable.</summary>
        ///<param name="items">The enumerable to search.</param>
        ///<param name="predicate">The expression to test the items against.</param>
        ///<returns>The index of the first matching item, or -1 if no items match.</returns>
        [DebuggerStepThrough]
        public static int IndexOf<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (predicate == null) throw new ArgumentNullException("predicate");

            int retVal = 0;
            foreach (var item in items)
            {
                if (predicate(item)) return retVal;
                retVal++;
            }

            return -1;
        }

        ///<summary>Finds the index of the first occurrence of an item in an enumerable.</summary>
        ///<param name="items">The enumerable to search.</param>
        ///<param name="item">The item to find.</param>
        ///<returns>The index of the first matching item, or -1 if the item was not found.</returns>
        [DebuggerStepThrough]
        public static int IndexOf<T>(this IEnumerable<T> items, T item)
        {
            return items.IndexOf(i => EqualityComparer<T>.Default.Equals(item, i));
        }


        /// <summary>Determines whether exists any element of a sequence that satisfies a condition.</summary>
        /// <param name="source">
        ///     An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to apply the predicate
        ///     to.
        /// </param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>
        ///     true if exists any element in the source sequence that passed the test in the specified predicate; otherwise,
        ///     false.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="predicate" /> is null.
        /// </exception>
        [DebuggerStepThrough]
        public static bool Exists<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Any(predicate);
        }

        /// <summary>
        ///     Returns <c>true</c> if sequence is empty, otherwise returns <c>false</c>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}" /> that contains the elements to be counted.</param>
        /// <returns><c>true</c> if length is 0, else <c>false</c>.</returns>
        [DebuggerStepThrough]
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return source.LongCount().Equals(0L);
        }

        /// <summary>
        ///     Returns <c>true</c> if sequence is empty, otherwise returns <c>false</c>
        /// </summary>
        /// <param name="source"></param>
        /// <returns><c>true</c> if length is 0, else <c>false</c>.</returns>
        [DebuggerStepThrough]
        public static bool IsEmpty(this IEnumerable source)
        {
            return source.Cast<object>().LongCount().Equals(0L);
        }

        /// <summary>
        ///     Performs the specified action on each element of the <see cref="IEnumerable{T}" />.
        /// </summary>
        /// <typeparam name="T">Type of elements in <paramref name="elements" /></typeparam>
        /// <param name="elements">
        ///     <see cref="IEnumerable{T}" /> sequence of values on which should be performed the
        ///     <paramref name="action" />
        /// </param>
        /// <param name="action">
        ///     The <see cref="Action{T}" /> delegate to perform on each element of the
        ///     <see cref="IEnumerable{T}" />.
        /// </param>
        /// <exception cref="ArgumentNullException">Passed <paramref name="action" /> is null</exception>
        /// <returns>Self reference to <paramref name="elements" /></returns>
        [DebuggerStepThrough]
        public static IEnumerable<T> ForEachDo<T>(this IEnumerable<T> elements, Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            foreach (T element in elements)
                action(element);

            return elements;
        }

        /// <summary>
        ///     Aggregates an <paramref name="source" /> of type <see cref="IEnumerable{TSource}" /> to a comma-separated
        ///     <see cref="string" />.
        /// </summary>
        /// <param name="source">
        ///     <see cref="string" />s collection plainTextSource that will be aggregated into a single comma-separated
        ///     <see cref="string" />
        /// </param>
        /// <returns>Comma-separated string</returns>
        [DebuggerStepThrough]
        public static string ToCommaSeparatedString<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null || !source.Any()) return string.Empty;

            return source.Aggregate(
                string.Empty,
                (accumulator, item) => $"{accumulator}, {item}",
                accumulator => accumulator.Substring(2, accumulator.Length - 2));
        }

        /// <summary>
        ///     Aggregates an <paramref name="source" /> of type <see cref="IEnumerable{TSource}" /> to a comma-separated
        ///     <see cref="string" />.
        /// </summary>
        /// <param name="source">
        ///     <see cref="string" />s collection plainTextSource that will be aggregated into a single comma-separated
        ///     <see cref="string" />
        /// </param>
        /// <returns>Comma separated string</returns>
        [DebuggerStepThrough]
        public static string ToCommaSeparatedString<TSource>(params TSource[] source)
        {
            return source.ToCommaSeparatedString();
        }

        /// <summary>
        ///     Aggregates an <paramref name="source" /> of type <see cref="IEnumerable{TSource}" /> to a comma-separated
        ///     <see cref="string" /> ending with a dot.
        /// </summary>
        /// <param name="source">
        ///     <see cref="string" />s collection plainTextSource that will be aggregated into a single comma-separated
        ///     <see cref="string" /> with a dot at the end.
        /// </param>
        /// <returns>Comma-separated string</returns>
        [DebuggerStepThrough]
        public static string ToCommaSeparatedStringWithEndingDot<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null || !source.Any()) return string.Empty;

            return source.Aggregate(
                string.Empty,
                (accumulator, item) => $"{accumulator}, {item}",
                accumulator => $"{accumulator.Substring(2, accumulator.Length - 2)}.");
        }

        /// <summary>
        ///     Aggregates an <paramref name="source" /> of type <see cref="IEnumerable{TSource}" /> to a comma-separated
        ///     <see cref="string" /> ending with a dot.
        /// </summary>
        /// <param name="source">
        ///     <see cref="string" />s collection plainTextSource that will be aggregated into a single comma-separated
        ///     <see cref="string" /> with a dot at the end.
        /// </param>
        /// <returns>Comma separated string</returns>
        [DebuggerStepThrough]
        public static string ToCommaSeparatedStringWithEndingDot<TSource>(params TSource[] source)
        {
            return source.ToCommaSeparatedStringWithEndingDot();
        }

        /// <summary>
        ///     Aggregates an <paramref name="source" /> of type <see cref="IEnumerable{TSource}" /> to an JS-like array
        ///     representation.
        /// </summary>
        /// <param name="source">
        ///     <see cref="string" />s collection plainTextSource that will be aggregated into a single comma-separated
        ///     <see cref="string" /> with a dot at the end.
        /// </param>
        /// <returns><see cref="string" /> as an JS-like array representation.</returns>
        /// <summary>
        ///     Aggregates an <paramref name="source" /> of type <see cref="IEnumerable{TSource}" /> to an JS-like array
        ///     representation.
        /// </summary>
        /// <param name="source">
        ///     <see cref="string" />s collection plainTextSource that will be aggregated into a single comma-separated
        ///     <see cref="string" /> with a dot at the end.
        /// </param>
        /// <returns><see cref="string" /> as an JS-like array representation.</returns>
        /// <summary>
        ///     Reduces an <see cref="IEnumerable{T}">IEnumerable&lt;char&gt;</see>
        ///     to a <see cref="string" />.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string AggregateToString(this IEnumerable<char> source)
        {
            return source.Aggregate(new StringBuilder(), (builder, character) => builder.Append(character)).ToString();
        }


        /// <summary>
        ///     Performs the specified action on each element of the <see cref="IEnumerable{T}" />. Adds details about each current
        ///     item (like "Index","IsFirst","IsLast").
        /// </summary>
        /// <typeparam name="TItem">Type of elements in <paramref name="elements" /></typeparam>
        /// <param name="elements">
        ///     <see cref="IEnumerable{T}" /> sequence of values on which should be performed the
        ///     <paramref name="action" />
        /// </param>
        /// <param name="action">
        ///     The <see cref="Action{T}" /> delegate to perform on each element of the
        ///     <see cref="IEnumerable{T}" /> and expose additional info about current item (like "Index","IsFirst","IsLast").
        /// </param>
        /// <returns>Self reference to <paramref name="elements" /></returns>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="elements" /> is <see langword="null" /></exception>
        [DebuggerStepThrough]
        public static IEnumerable<TItem> ForEachWithDetailsDo<TItem>(this IEnumerable<TItem> elements, ForEachAction<TItem> action)
        {
            if (elements == null) throw new ArgumentNullException(nameof(elements));

            using (IEnumerator<TItem> enumerator = elements.GetEnumerator())
            {
                bool isFirst = true;
                bool hasNext = enumerator.MoveNext();
                int index = -1;
                while (hasNext)
                {
                    TItem current = enumerator.Current;
                    hasNext = enumerator.MoveNext();
                    var itemInfo = new ItemInfo(++index, isFirst, !hasNext);
                    action?.Invoke(current, itemInfo);
                    isFirst = false;
                }
            }

            return elements;
        }

        /// <summary>
        ///     Adds additional properties (Index, IsFirst, IsLast) to the item, by enclosing it in a
        ///     <see cref="IterationEntry{T}" /> wrapper.
        /// </summary>
        /// <typeparam name="TItem">The original item's type.</typeparam>
        /// <param name="source">The original <see cref="IEnumerable{T}" /> source.</param>
        /// <returns>
        ///     <see cref="IEnumerable{T}">IEnumerable&lt;IterationEntry&lt;TItem&gt;&gt;</see>
        /// </returns>
        [DebuggerStepThrough]
        public static IEnumerable<IterationEntry<TItem>> WithDetails<TItem>(this IEnumerable<TItem> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            using (var enumerator = source.GetEnumerator())
            {
                bool isFirst = true;
                bool hasNext = enumerator.MoveNext();
                int index = -1;
                while (hasNext)
                {
                    TItem current = enumerator.Current;
                    hasNext = enumerator.MoveNext();
                    yield return new IterationEntry<TItem>(++index, current, isFirst, !hasNext);
                    isFirst = false;
                }
            }
        }

        /// <summary>
        ///     The <see cref="Action{T}" /> delegate to perform on each element of the <see cref="IEnumerable{T}" /> and expose
        ///     additional info about current item (like "Index","IsFirst","IsLast").
        /// </summary>
        /// <typeparam name="TItem">Item type.</typeparam>
        /// <param name="item">Current item.</param>
        /// <param name="info">Additional info about current item (like "Index","IsFirst","IsLast").</param>
        public delegate void ForEachAction<in TItem>(TItem item, ItemInfo info);

        //public static void ForEach<TItem>(this IEnumerable<TItem> elements, Action<TItem, int> action)
        //{
        //    using (IEnumerator<TItem> enumerator = elements.GetEnumerator())
        //    {
        //        bool hasNext = enumerator.MoveNext();
        //        int index = 0;
        //        while (hasNext)
        //        {
        //            TItem current = enumerator.Current;
        //            hasNext = enumerator.MoveNext();
        //            action(current, ++index);
        //        }
        //    }
        //}

        public struct ItemInfo
        {
            public int Index { get; }
            public bool IsFirst { get; }
            public bool IsLast { get; }

            public ItemInfo(int index, bool isFirst, bool isLast) : this()
            {
                Index = index;
                IsFirst = isFirst;
                IsLast = isLast;
            }
        }

        public struct IterationEntry<T>
        {
            public int Index { get; }
            public bool IsFirst { get; }
            public bool IsLast { get; }
            public T Value { get; }

            public IterationEntry(int index, T value, bool isFirst, bool isLast) : this()
            {
                Index = index;
                IsFirst = isFirst;
                IsLast = isLast;
                Value = value;
            }

            public override string ToString() => $"{Value}";
        }
    }
}