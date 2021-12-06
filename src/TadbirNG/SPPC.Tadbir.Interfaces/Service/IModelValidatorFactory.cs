using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPPC.Tadbir.Configuration.Enums;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModelValidatorFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        IModelValidator Create(EditionLimit limit);
    }
}
