﻿namespace NativeCode.Core.Extensions
{
    using System;
    using JetBrains.Annotations;

    public static class UriExtensions
    {
        [CanBeNull]
        public static string Login([NotNull] this Uri source)
        {
            if (string.IsNullOrWhiteSpace(source.UserInfo))
            {
                return null;
            }

            if (source.UserInfo.Contains(":"))
            {
                return source.UserInfo.Split(":")[0];
            }

            return source.UserInfo;
        }

        [CanBeNull]
        public static string Password([NotNull] this Uri source)
        {
            if (string.IsNullOrWhiteSpace(source.UserInfo))
            {
                return null;
            }

            if (source.UserInfo.Contains(":"))
            {
                return source.UserInfo.Split(":")[1];
            }

            return null;
        }
    }
}