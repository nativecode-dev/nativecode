﻿namespace Common.Data
{
    using System.Data.Entity;
    using System.Linq;

    using Common.Data.Entities;
    using Common.Data.Entities.Navigation;

    using NativeCode.Core.DotNet.Platform;
    using NativeCode.Core.DotNet.Providers;
    using NativeCode.Core.Platform;
    using NativeCode.Core.Platform.Connections;
    using NativeCode.Core.Platform.Security;
    using NativeCode.Packages.Data;

    public class CoreDataContext : DbDataContext
    {
        public CoreDataContext()
            : base(new ConnectionStringProvider(), new DotNetPlatform(Enumerable.Empty<IAuthenticationProvider>()))
        {
        }

        public CoreDataContext(IConnectionStringProvider provider, IPlatform platform)
            : base(provider, platform)
        {
        }

        public IDbSet<Account> Accounts { get; set; }

        public IDbSet<Download> Downloads { get; set; }

        public IDbSet<MenuAction> MenuActions { get; set; }

        public IDbSet<Storage> Storage { get; set; }

        public IDbSet<Token> Tokens { get; set; }
    }
}