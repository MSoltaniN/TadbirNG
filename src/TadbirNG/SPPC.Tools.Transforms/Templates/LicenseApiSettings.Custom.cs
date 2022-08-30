﻿using SPPC.Framework.Utility;
using SPPC.Tadbir.Utility;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class LicenseApiSettings : ITextTemplate
    {
        public LicenseApiSettings(IBuildSettings settings)
        {
            _settings = settings;
        }

        private readonly IBuildSettings _settings;
    }
}
