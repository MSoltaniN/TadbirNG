using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    public sealed class FilterApi
    {
        private FilterApi()
        {
        }

        public const string FiltersByView = "filters/views/{0}";

        public const string FiltersByViewUrl = "filters/views/{viewId:min(1)}";

        public const string Filters = "filters";

        public const string FiltersUrl = "filters";

        public const string Filter = "filters/{0}";

        public const string FilterUrl = "filters/{filterId:min(1)}";
    }
}
