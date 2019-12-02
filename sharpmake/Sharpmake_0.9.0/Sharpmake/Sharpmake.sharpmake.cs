﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Sharpmake;

namespace SharpmakeGen
{
    [Generate]
    public class SharpmakeProject : Common.SharpmakeBaseProject
    {
        public SharpmakeProject()
        {
            Name = "Sharpmake";
        }

        public override void ConfigureAll(Configuration conf, Target target)
        {
            base.ConfigureAll(conf, target);
            conf.ReferencesByNameExternal.Add("Microsoft.Build.Utilities.v4.0");

            conf.Options.Add(Options.CSharp.AllowUnsafeBlocks.Enabled);
            conf.ReferencesByNuGetPackage.Add("Microsoft.VisualStudio.Setup.Configuration.Interop", "1.16.30");
        }
    }
}
