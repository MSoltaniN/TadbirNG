using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    ///
    /// </summary>
    public interface ISystemErrorRepository
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="error"></param>
        Task SaveAsync(SystemErrorViewModel error);
    }
}
