﻿namespace NativeCode.Web.AspNet.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using NativeCode.Core.Logging;

    public abstract class BaseController : ApiController
    {
        private readonly List<IDisposable> disposables = new List<IDisposable>();

        protected BaseController(ILogger logger)
        {
            this.Logger = logger;
        }

        protected ILogger Logger { get; }

        protected void Disposable<T>(T disposable) where T : IDisposable
        {
            this.disposables.Add(disposable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.disposables.Any())
            {
                foreach (var disposable in this.disposables)
                {
                    disposable.Dispose();
                }

                this.disposables.Clear();
            }

            base.Dispose(disposing);
        }
    }
}
