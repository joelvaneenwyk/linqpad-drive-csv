﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security;

// ReSharper disable UnusedType.Global
// ReSharper disable once UnusedMember.Global

namespace LPRun
{
    public static class ConnectionHeader
    {
        public static string Get<T>(string driverAssemblyName, string driverNamespace, T driverConfig, params string[] additionalNamespaces)
            where T : notnull =>
            $@"<Query Kind=""Statements"">
  <Connection>
    <Driver Assembly=""{driverAssemblyName}"">{driverNamespace}</Driver>
    <DriverData>
{string.Join(Environment.NewLine, GetKeyValues(driverConfig).Select(keyValuePair => $"      <{keyValuePair.Key}>{SecurityElement.Escape(keyValuePair.Value)}</{keyValuePair.Key}>"))}
    </DriverData>
  </Connection>
  <NuGetReference>FluentAssertions</NuGetReference>
{string.Join(Environment.NewLine, new[] { "FluentAssertions" }.Concat(additionalNamespaces).Select(additionalNamespace => $"  <Namespace>{additionalNamespace}</Namespace>"))}
</Query>";

        private static IEnumerable<(string Key, string Value)> GetKeyValues<T>(T driverConfig)
            where T : notnull
        {
            return driverConfig.GetType().GetProperties()
                .Where(propertyInfo => propertyInfo.CanRead && propertyInfo.CanWrite)
                .Select(propertyInfo => (propertyInfo.Name, ValueToString(propertyInfo)));

            string ValueToString(PropertyInfo propertyInfo) =>
                propertyInfo.GetValue(driverConfig) switch
                {
                    null => string.Empty,
                    string v => v,
                    char v => v.ToString(),
                    Enum v => v.ToString(),
                    int v => v.ToString(CultureInfo.InvariantCulture),
                    bool v => v ? "true" : "false",
                    _ => throw new NotSupportedException($"Could not convert {propertyInfo.Name} of type {propertyInfo.PropertyType} to string")
                };
        }
    }
}