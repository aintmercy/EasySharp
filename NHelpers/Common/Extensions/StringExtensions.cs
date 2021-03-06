﻿namespace EasySharp.NHelpers.Common.Extensions
{
    using System;
    using System.Text;

    public static class StringExtensions
    {
        /// <summary>Returns a value indicating whether a specified substring occurs within this string.</summary>
        /// <param name="text">Text in which has to be searched the key string <paramref name="value" /> </param>
        /// <param name="value">The string to seek. </param>
        /// <returns>
        ///     <see langword="true" /> if the <paramref name="value" /> parameter occurs within this string, or if
        ///     <paramref name="value" /> is the empty string (""); otherwise, <see langword="false" />.
        /// </returns>
        /// <remarks>Cannot be applied in case of LINQ to Entities query.</remarks>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="value" /> is <see langword="null" />.
        /// </exception>
        public static bool ContainsIgnoreCase(this string text, string value)
        {
            return text.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }


        /// <summary>
        ///     Counts occurrences of <paramref name="subString" /> is <paramref name="source" />
        /// </summary>
        /// <remarks>Default string comparison mode is <see cref="StringComparison.OrdinalIgnoreCase" /></remarks>
        /// <param name="source"></param>
        /// <param name="subStr"></param>
        /// <returns>Number of occurrences of <paramref name="subStr" /> is <paramref name="source" /></returns>
        public static int CountOccurencesOrdinalIgnoreCase(this string source, string subStr)
        {
            return CountOccurenceInSource(source, subStr, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     Counts occurrences of <paramref name="subString" /> is <paramref name="source" />
        /// </summary>
        /// <remarks>Default string compassion mode is <see cref="StringComparison.InvariantCultureIgnoreCase" /></remarks>
        /// <param name="source"></param>
        /// <param name="subString"></param>
        /// <returns>Number of occurrences of <paramref name="subString" /> is <paramref name="source" /></returns>
        public static int CountOccurencesInvariantCultureIgnoreCase(this string source, string subString)
        {
            return CountOccurenceInSource(source, subString, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        ///     Counts occurrences of <paramref name="subString" /> is <paramref name="source" />
        /// </summary>
        /// <remarks>Default string comparison mode is <see cref="StringComparison.Ordinal" /></remarks>
        /// <param name="source"></param>
        /// <param name="subString"></param>
        /// <returns>Number of occurrences of <paramref name="subString" /> is <paramref name="source" /></returns>
        public static int CountOccurencesOrdinal(this string source, string subString)
        {
            return CountOccurenceInSource(source, subString, StringComparison.Ordinal);
        }

        /// <summary>
        ///     Counts occurrences of <paramref name="subString" /> is <paramref name="source" />
        /// </summary>
        /// <remarks>Default string comparison mode is <see cref="StringComparison.InvariantCulture" /></remarks>
        /// <param name="source"></param>
        /// <param name="subString"></param>
        /// <returns>Number of occurrences of <paramref name="subString" /> is <paramref name="source" /></returns>
        public static int CountOccurencesInvariantCulture(this string source, string subString)
        {
            return CountOccurenceInSource(source, subString, StringComparison.InvariantCulture);
        }


        /// <summary>
        ///     Counts occurrences of <paramref name="subString" /> is <paramref name="source" />
        /// </summary>
        /// <remarks>Default string comparison mode is <see cref="StringComparison.Ordinal" /></remarks>
        /// <param name="source"></param>
        /// <param name="subString">search key</param>
        /// <param name="stringComparisonMode"></param>
        /// <returns>Number of occurrences of <paramref name="subString" /> is <paramref name="source" /></returns>
        public static int CountOccurences(this string source, string subString,
            StringComparison stringComparisonMode = StringComparison.Ordinal)
        {
            return CountOccurenceInSource(source, subString, stringComparisonMode);
        }

        /// <summary>
        ///     Decodes the encoded <paramref name="base64EncodedDataSource" /> by Base64 (MIME) scheme.
        /// </summary>
        /// <remarks>You should use Base64 whenever you intend to transmit binary data in a textual format.</remarks>
        /// <param name="base64EncodedDataSource">Encoded <see cref="string" /> by Base64 scheme.</param>
        /// <returns></returns>
        public static string ToDecodedStringFromBase64(this string base64EncodedDataSource)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedDataSource));
        }

        /// <summary>
        ///     Encodes <paramref name="plainTextSource" /> to the Base64 (MIME)
        /// </summary>
        /// <remarks>You should use Base64 whenever you intend to transmit binary data in a textual format.</remarks>
        /// <param name="plainTextSource">A plain UTF8 encoded <see cref="string" /></param>
        /// <returns></returns>
        public static string ToBase64String(this string plainTextSource)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainTextSource));
        }

        /// <summary>
        ///     Converts and gets ASCII Encoded Bytes out of a string (<paramref name="source" />)
        /// </summary>
        /// <param name="source">Source string</param>
        /// <returns>Array of bytes</returns>
        public static byte[] ToAsciiEncodedByteArray(this string source)
        {
            return Encoding.ASCII.GetBytes(source.ToCharArray());
        }

        /// <summary>
        ///     Converts and gets byte array into ASCII Encoded string
        /// </summary>
        /// <param name="sourceBytes"></param>
        /// <returns></returns>
        public static string ToAsciiString(this byte[] sourceBytes)
        {
            return Encoding.ASCII.GetString(sourceBytes);
        }

        /// <summary>
        ///     Converts and gets UTF8 Encoded Bytes out of a string (<paramref name="source" />)
        /// </summary>
        /// <param name="source">Source string</param>
        /// <returns>Array of bytes</returns>
        public static byte[] ToUtf8EncodedByteArray(this string source)
        {
            return Encoding.UTF8.GetBytes(source);
        }

        /// <summary>
        ///     Converts and gets byte array into UTF8 Encoded string
        /// </summary>
        /// <param name="sourceBytes"></param>
        /// <returns></returns>
        public static string ToUtf8String(this byte[] sourceBytes)
        {
            return Encoding.UTF8.GetString(sourceBytes);
        }

        /// <summary>
        ///     Writes the text representation of the given value be calling the <see langword="ToString" /> method on that value,
        ///     followed by a line terminator to the text string or stream.
        /// </summary>
        /// <param name="value">
        ///     The value to write on console. If <paramref name="value" /> is <see langword="null" />, only the line
        ///     terminator is written.
        /// </param>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        [Obsolete("Not Tested", true)]
        public static void Print(this string value)
        {
            Console.Out.WriteLine(value);
        }

        /// <summary>
        ///     Writes the text representation of the given value be calling the <see langword="ToString" /> method on that value,
        ///     followed by a line terminator to the text string or stream.
        /// </summary>
        /// <param name="value">
        ///     The value to write on console. If <paramref name="value" /> is <see langword="null" />, only the line
        ///     terminator is written.
        /// </param>
        /// <param name="format">A composite format <see cref="string" />.</param>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        [Obsolete("Not Tested", true)]
        public static void Print(this string value, string format)
        {
            Console.Out.WriteLine(format, value);
        }

        /// <summary>
        ///     Counts occurrences of <paramref name="subString" /> is <paramref name="source" />
        /// </summary>
        /// <param name="source"></param>
        /// <param name="subString"></param>
        /// <param name="mode"></param>
        /// <returns>Number of occurrences of <paramref name="subStr" /> is <paramref name="source" /></returns>
        private static int CountOccurenceInSource(string source, string subString, StringComparison mode)
        {
            int occurencesCount = 0;

            for (int indexPosition = 0;; indexPosition += subString.Length)
            {
                indexPosition = source.IndexOf(subString, indexPosition, mode);

                // searchKey isn't contained by the given substring (indexPosition..plainTextSource.Length-1)
                if (indexPosition == -1) break;

                // searchKey IS contained by the given substring (indexPosition..plainTextSource.Length-1)
                ++occurencesCount;
            }

            return occurencesCount;
        }
    }
}