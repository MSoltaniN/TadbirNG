namespace SPPC.Tadbir.Business
{
    using System.Collections.Generic;

    /// <summary>
    /// Grid option for lazyloading
    /// </summary>
    public class GridOption
    {
        /// <summary>
        /// Gets or sets Start Index
        /// </summary>
        public int? StartIndex { get; set; }

        /// <summary>
        ///  Gets or sets Count
        /// </summary>
        public int? Count { get; set; }

        /// <summary>
        /// Gets or sets Filter
        /// </summary>
        public IList<Filter> Filters { get; set; }

        /// <summary>
        /// Gets or sets OrderBy
        /// </summary>
        public string OrderBy { get; set; }
    }    
}
