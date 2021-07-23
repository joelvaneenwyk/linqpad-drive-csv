﻿using System;
using System.Collections.Generic;
using System.Linq;

using Humanizer;

namespace CsvLINQPadDriver.Extensions
{
    internal static class TextExtensions
    {
        public static string JoinNewLine(this string? first, params string?[] other) =>
            JoinNewLine(new[] { first }.Concat(other));

        public static string JoinNewLine(this IEnumerable<string?> other) =>
            string.Join(Environment.NewLine, other);

        public static string Pluralize<T>(this IReadOnlyCollection<T> collection, string what, string? fallback = null) =>
            collection.Count > 1
                ? fallback ?? what.Pluralize()
                : what;
    }
}