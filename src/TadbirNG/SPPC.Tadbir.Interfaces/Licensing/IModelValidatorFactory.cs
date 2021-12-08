using SPPC.Tadbir.Configuration.Enums;

namespace SPPC.Tadbir.Licensing
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
