using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.NHibernate
{
    /// <summary>
    /// Defines repository operations related to security administration.
    /// </summary>
    public interface ISecurityRepository
    {
        /// <summary>
        /// Retrieves all application users from repository.
        /// </summary>
        /// <returns>A collection of <see cref="UserViewModel"/> objects retrieved from repository</returns>
        IList<UserViewModel> GetUsers();
    }
}
