﻿namespace EasySharp.NHelpers.Collections.Extensions
{
    using System;

    public static class ArrayExtensions
    {
        /// <summary>
        ///     Sets each element in the <paramref name="sourceArray" /> (of type <c>T</c>) to its default value (<c>default(T)</c>
        ///     )
        /// </summary>
        /// <typeparam name="T">Any available type</typeparam>
        /// <param name="sourceArray">Array which elements should be initialized or set to defaults (<c>default(T)</c>)</param>
        /// <remarks>Performance-oriented algorithm selection.</remarks>
        public static void SelfSetToDefaults<T>(this T[] sourceArray)
        {
            if (sourceArray.Length <= 76)
            {
                for (int i = 0; i < sourceArray.Length; i++)
                {
                    sourceArray[i] = default(T);
                }
            }
            else
            { // 77+
                Array.Clear(
                    array: sourceArray,
                    index: 0,
                    length: sourceArray.Length);
            }
        }

        /// <summary>
        ///     Sets each element in the copy of <paramref name="sourceArray" /> (of type <c>T</c>) to its default value (
        ///     <c>default(T)</c>)
        /// </summary>
        /// <typeparam name="T">Any available type</typeparam>
        /// <param name="sourceArray">Array which elements should be initialized or set to defaults (<c>default(T)</c>)</param>
        /// <returns><see cref="Array" /> of type <c>T</c></returns>
        public static T[] SetToDefaults<T>(this T[] sourceArray)
        {
            T[] newArray = new T[sourceArray.Length];

            for (int i = 0; i < sourceArray.Length; i++)
                newArray[i] = default(T);

            return newArray;
        }

        /// <summary>
        ///     Clones source array by creating a new array and copying all the elements from source array to destination array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T[] CloneArray<T>(this T[] source)
        {
            T[] destination = new T[source.Length];
            Array.Copy(source, destination, source.Length);
            return destination;
        }
    }
}