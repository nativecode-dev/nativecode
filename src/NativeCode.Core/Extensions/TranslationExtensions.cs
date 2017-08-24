﻿namespace NativeCode.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Localization.Translation.Attributes;

    public static class TranslationExtensions
    {
        public static bool IsClassTranslatable(this Type type)
        {
            var info = type.GetTypeInfo();

            if (info.IsClass == false)
            {
                return false;
            }

            var classTranslatable = info.GetCustomAttribute<TranslateAttribute>() != null;
            var classIgnored = info.GetCustomAttribute<IgnoreTranslateAttribute>() != null;

            return classTranslatable && classIgnored == false;
        }

        public static bool IsPropertyTranslatableIgnored(this PropertyInfo property)
        {
            return property.IsPropertyTranslatable() == false;
        }

        public static bool IsPropertyTranslatable(this PropertyInfo property)
        {
            var classTranslatable = property.DeclaringType.IsClassTranslatable();
            var propertyTranslatable = property.GetCustomAttribute<TranslateAttribute>() != null;
            var propertyIgnored = property.GetCustomAttribute<IgnoreTranslateAttribute>() != null;

            return (classTranslatable || propertyTranslatable) && propertyIgnored == false;
        }

        public static IEnumerable<PropertyInfo> GetTranslatableProperties(this Type type)
        {
            var properties = from property in type.GetRuntimeProperties()
                             where property.IsPropertyTranslatable()
                             select property;

            if (type.IsClassTranslatable())
            {
                return properties;
            }

            return properties.Where(property => property.GetCustomAttribute<TranslateAttribute>() != null);
        }
    }
}