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

        /// <summary>
        /// Retrieves a single user specified by user name from repository.
        /// </summary>
        /// <param name="userName">User name to search for</param>
        /// <returns>A <see cref="UserViewModel"/> instance that corresponds to the specified user name, if there is
        /// such a user defined; otherwise, returns null.</returns>
        UserViewModel GetUser(string userName);
    }
}
