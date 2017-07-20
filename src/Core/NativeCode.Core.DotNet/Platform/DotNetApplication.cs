﻿namespace NativeCode.Core.DotNet.Platform
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Core.Platform;
    using Core.Settings;
    using Extensions;

    public class DotNetApplication : Application
    {
        public DotNetApplication(IPlatform platform, Settings settings)
            : base(platform, settings)
        {
            this.ApplicationPath = this.GetApplicationPath();
        }

        protected string GetApplicationPath()
        {
            try
            {
                var path = Path.Combine(this.Platform.DataPath, this.GetApplicationName());

                if (Directory.Exists(path) == false)
                    Directory.CreateDirectory(path);

                return path;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToExceptionString());
                throw;
            }
        }
    }
}