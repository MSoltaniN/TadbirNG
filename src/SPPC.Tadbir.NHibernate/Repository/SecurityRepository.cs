using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.ViewModel.Auth;
using SwForAll.Platform.Persistence;

namespace SPPC.Tadbir.NHibernate
{
    /// <summary>
    /// Provides repository operations related to security administration.
    /// </summary>
    public class SecurityRepository : ISecurityRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The <see cref="IUnitOfWork"/> implementation to use for all database operations
        /// in this repository.</param>
        /// <param name="mapper">Domain mapper to use for mapping between entitiy and view model classes</param>
        public SecurityRepository(IUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all application users from repository.
        /// </summary>
        /// <returns>A collection of <see cref="UserViewModel"/> objects retrieved from repository</returns>
        public IList<UserViewModel> GetUsers()
        {
            var repository = _unitOfWork.GetRepository<User>();
            var users = repository
                .GetAll()
                .Select(user => _mapper.Map<UserViewModel>(user))
                .ToList();
            return users;
        }

        /// <summary>
        /// Retrieves a single user specified by user name from repository.
        /// </summary>
        /// <param name="userName">User name to search for</param>
        /// <returns>A <see cref="UserViewModel"/> instance that corresponds to the specified user name, if there is
        /// such a user defined; otherwise, returns null.</returns>
        public UserViewModel GetUser(string userName)
        {
            UserViewModel userViewModel = null;
            var repository = _unitOfWork.GetRepository<User>();
            var user = repository
                .GetByCriteria(usr => usr.UserName == userName)
                .FirstOrDefault();
            if (user != null)
            {
                userViewModel = _mapper.Map<UserViewModel>(user);
            }

            return userViewModel;
        }

        private IUnitOfWork _unitOfWork;
        private IDomainMapper _mapper;
    }
}
