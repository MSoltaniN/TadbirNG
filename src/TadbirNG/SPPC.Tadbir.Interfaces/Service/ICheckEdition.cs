using SPPC.Tadbir.Configuration.Enums;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// عملیات مورد نیاز برای کنترل ویرایش برنامه را تعریف می کند
    /// </summary>
    public interface ICheckEdition
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        string ValidateNewModel(object model, EditionLimit limit);
    }
}
