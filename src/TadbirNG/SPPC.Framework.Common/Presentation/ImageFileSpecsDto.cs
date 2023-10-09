using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Framework.Presentation
{
    public class ImageFileSpecsDto
    {
        public ImageFileSpecsDto()
        {
            Name = string.Empty;
            XOssProcess = string.Empty;
            Quality = 0;
            ResizeHeight = 0;
            ResizeWidth = 0;
        }

        public int? Id { get; set; }
        public String? Name { get; set; }
        public String? XOssProcess { get; set; }
        public int ResizeWidth { get; set; }
        public int ResizeHeight { get; set; }
        public int Quality { get; set; }

    }
}
