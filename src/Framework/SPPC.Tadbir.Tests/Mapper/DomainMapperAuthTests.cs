using NUnit.Framework;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Mapper.Tests
{
    [TestFixture]
    [Category("DomainMapping")]
    public class DomainMapperAuthTests : DomainMapperTestBase
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            SetUp();
        }

        #region User Mapping Tests

        [Test]
        public void ContainsMappingFromUserToUserViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<User, UserViewModel>();
        }

        [Test]
        public void CanMapFromUserToUserViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<User, UserViewModel>();
        }

        [Test]
        public void ContainsMappingFromUserViewModelToUser()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<UserViewModel, User>();
        }

        [Test]
        public void CanMapFromUserViewModelToUser()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<UserViewModel, User>();
        }

        [Test]
        public void ContainsMappingFromUserToUserContextViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<User, UserContextViewModel>();
        }

        [Test]
        public void CanMapFromUserToUserContextViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<User, UserContextViewModel>();
        }

        [Test]
        public void ContainsMappingFromUserToUserBriefViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<User, UserBriefViewModel>();
        }

        [Test]
        public void CanMapFromUserToUserBriefViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<User, UserBriefViewModel>();
        }

        [Test]
        public void ContainsMappingFromUserBriefViewModelToUser()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<UserBriefViewModel, User>();
        }

        [Test]
        public void CanMapFromUserBriefViewModelToUser()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<UserBriefViewModel, User>();
        }

        [Test]
        public void ContainsMappingFromUserToRelatedItemsViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<User, RelatedItemsViewModel>();
        }

        [Test]
        public void CanMapFromUserToRelatedItemsViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<User, RelatedItemsViewModel>();
        }

        [Test]
        public void ContainsMappingFromUserToRelatedItemViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<User, RelatedItemViewModel>();
        }

        [Test]
        public void CanMapFromUserToRelatedItemViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<User, RelatedItemViewModel>();
        }

        #endregion // User Mapping Tests

        #region Role Mapping Tests

        [Test]
        public void ContainsMappingFromRoleToRoleViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Role, RoleViewModel>();
        }

        [Test]
        public void CanMapFromRoleToRoleViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Role, RoleViewModel>();
        }

        [Test]
        public void ContainsMappingFromRoleViewModelToRole()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<RoleViewModel, Role>();
        }

        [Test]
        public void CanMapFromRoleViewModelToRole()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<RoleViewModel, Role>();
        }

        [Test]
        public void ContainsMappingFromRoleToRelatedItemsViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Role, RelatedItemsViewModel>();
        }

        [Test]
        public void CanMapFromRoleToRelatedItemsViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Role, RelatedItemsViewModel>();
        }

        [Test]
        public void ContainsMappingFromRoleToRelatedItemViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Role, RelatedItemViewModel>();
        }

        [Test]
        public void CanMapFromRoleToRelatedItemViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Role, RelatedItemViewModel>();
        }

        [Test]
        public void ContainsMappingFromRoleToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Role, KeyValue>();
        }

        [Test]
        public void CanMapFromRoleToKeyValue()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Role, KeyValue>();
        }

        #endregion // Role Mapping Tests

        #region Permission Mapping Tests

        [Test]
        public void ContainsMappingFromPermissionToPermissionViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Permission, PermissionViewModel>();
        }

        [Test]
        public void CanMapFromPermissionToPermissionViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Permission, PermissionViewModel>();
        }

        [Test]
        public void ContainsMappingFromPermissionViewModelToPermission()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<PermissionViewModel, Permission>();
        }

        [Test]
        public void CanMapFromPermissionViewModelToPermission()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<PermissionViewModel, Permission>();
        }

        [Test]
        public void ContainsMappingFromPermissionToPermissionBriefViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<Permission, PermissionBriefViewModel>();
        }

        [Test]
        public void CanMapFromPermissionToPermissionBriefViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<Permission, PermissionBriefViewModel>();
        }

        #endregion // Role Mapping Tests

        #region ViewRowPermission Mapping Tests

        [Test]
        public void ContainsMappingFromViewRowPermissionToViewRowPermissionViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<ViewRowPermission, ViewRowPermissionViewModel>();
        }

        [Test]
        public void CanMapFromViewRowPermissionToViewRowPermissionViewModel()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<ViewRowPermission, ViewRowPermissionViewModel>();
        }

        [Test]
        public void ContainsMappingFromViewRowPermissionViewModelToViewRowPermission()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMappingIsDefined<ViewRowPermissionViewModel, ViewRowPermission>();
        }

        [Test]
        public void CanMapFromViewRowPermissionViewModelToViewRowPermission()
        {
            // Arrange (Done in FixtureSetup)

            // Act & Assert
            AssertMapperCanConvertFromSourceToDestination<ViewRowPermissionViewModel, ViewRowPermission>();
        }

        #endregion // ViewRowPermission Mapping Tests
    }
}
