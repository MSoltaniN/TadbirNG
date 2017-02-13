using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Mapper
{
    public interface IDomainMapper
    {
        object Configuration { get; }

        T Map<T>(object source);
    }
}
