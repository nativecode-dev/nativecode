﻿namespace NativeCode.Core.Extensions
{
    using Dependencies.Enums;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyLifetimeExtensions
    {
        public static ServiceLifetime Convert(this DependencyLifetime lifetime)
        {
            switch (lifetime)
            {
                case DependencyLifetime.PerApplication:
                    return ServiceLifetime.Singleton;

                case DependencyLifetime.PerContainer:
                    return ServiceLifetime.Scoped;

                default:
                    return ServiceLifetime.Transient;
            }
        }

        public static DependencyLifetime Convert(this ServiceLifetime lifetime)
        {
            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    return DependencyLifetime.PerApplication;

                case ServiceLifetime.Scoped:
                    return DependencyLifetime.PerContainer;

                default:
                    return DependencyLifetime.PerCall;
            }
        }
    }
}