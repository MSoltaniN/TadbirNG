﻿using System;
using SPPC.Framework.Utility;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class DockerComposeOverride : ITextTemplate
    {
        public DockerComposeOverride(string editionTag)
        {
            _editionTag = editionTag;
        }

        private readonly string _editionTag;
    }
}
