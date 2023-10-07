using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Moq;
using NUnit.Framework;
using SPPC.Tadbir.Model.ProductScope;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.ProductScope;
using SPPC.Tadbir.Web.Api.Controllers.ProductScope;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Web.Api.Controllers.Tests
{
    [TestFixture]
    [Category("WebApi")]
    public partial class UnitsControllerTests : ApiControllerTestBase<UnitsController>
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            _mockRepository = new Mock<IUnitRepository>();
            _mockAppUnitOfWorks = new Mock<IAppUnitOfWork>();
            _mockLocalizer = new Mock<IStringLocalizer<AppStrings>>();
        }

        [SetUp]
        public void Setup()
        {
            _mockLocalizer.Setup(loc => loc[It.IsAny<string>()])
                .Returns(new LocalizedString("Name", "Value"));

            _controller = new UnitsController(
                _mockRepository.Object,_mockLocalizer.Object, null)
            {
                ControllerContext = TestControllerContext
            };

            _existingUnit = new UnitViewModel()
            {
               
            };
        }

        [Test]
        public async Task GetUnitAsync_GivenExistingId_ReturnsJsonWithCorrectContentType()
        {
            // Arrange
            //_mockRepository.Setup(repo => repo.GetUnitAsync(_existingId))
            //    .ReturnsAsync(new UnitViewModel());
            
            _mockAppUnitOfWorks.Setup(repo => repo.GetAsyncRepository<Unit>().GetByIDAsync(_existingId))
                .ReturnsAsync(new Unit());

            // Act
            var result = await _controller.GetUnitAsync(_existingId) as JsonResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.InstanceOf<UnitViewModel>());
        }


        private Mock<IUnitRepository> _mockRepository;
        private Mock<IAppUnitOfWork> _mockAppUnitOfWorks;
        private Mock<IStringLocalizer<AppStrings>> _mockLocalizer;
        private UnitViewModel _existingUnit;
        private readonly int _existingId = 1;

    }
}
