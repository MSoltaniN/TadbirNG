using System;
using System.Collections.Generic;

namespace SPPC.Framework.Mapper
{
    public interface IDomainMapper
    {
        object Configuration { get; }

        T Map<T>(object source);
    }
}
