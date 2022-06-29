﻿using System.IO;

namespace SPPC.Tools.Model
{
    public static class PathConfig
    {
        public static string SolutionRoot => Path.Combine("..", "..", "..", "src", "TadbirNG");

        public static string LocalServerRoot => Path.Combine(SolutionRoot, "SPPC.Licensing.Local.Web");

        public static string WebApiRoot => Path.Combine(SolutionRoot, "SPPC.Tadbir.Web.Api");

        public static string WebEnvRoot => Path.Combine(SolutionRoot, "SPPC.Tadbir.Web", "ClientApp", "src", "environments");

        public static string EditionConfig => Path.Combine(SolutionRoot, "SPPC.Tools.LicenseManager", "edition-config.json");

        public static string ServicePublishWin => Path.Combine("..", "..", "Release", "net5.0", "publish", "win-x64");

        public static string RunnerPublishWin => Path.Combine("..", "..", "Release", "net5.0-windows", "publish", "win-x64");

        public static string TadbirRelease => Path.Combine("..", "..", "..", "..", "TadbirNG Release");
    }
}