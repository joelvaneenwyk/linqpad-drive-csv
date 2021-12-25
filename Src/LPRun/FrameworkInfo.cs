﻿using System;
using System.Runtime.InteropServices;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace LPRun
{
    public static class FrameworkInfo
    {
        public static bool IsNetFramework { get; }
        public static bool IsNet { get; }
        public static bool IsNetCore { get; }
        public static bool IsNetNative { get; }

        public static Version Version { get; }

        static FrameworkInfo()
        {
            IsNetFramework = Is(".NET Framework");
            IsNetCore      = Is(".NET Core");
            IsNetNative    = Is(".NET Native");
            IsNet          = Is(".NET"); // Should be last.

            Version = Environment.Version;

            static bool Is(string what) =>
                RuntimeInformation.FrameworkDescription.StartsWith(what);
        }
    }
}