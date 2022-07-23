﻿using System;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms;

namespace SPPC.Tools.Utility
{
    public class DbServiceSetup : DockerServiceSetup
    {
        public DbServiceSetup(IBuildSettings settings)
            : base(settings)
        {
        }

        protected override string ServiceName => DockerService.DbServerImage;

        protected override ITextTemplate SettingsTemplate => null;

        public override void ConfigureService(string imageRoot, string dockerPath)
        {
            var currentDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = imageRoot;
            Environment.SetEnvironmentVariable("Path", dockerPath, EnvironmentVariableTarget.Process);
            var imageFileName = String.Format($"{ServiceName}.tar.gz");
            _runner.Run(String.Format($"docker load -i {imageFileName}"));
            Environment.CurrentDirectory = currentDir;
        }
    }
}
