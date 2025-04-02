using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using ZorgmaatjeAPI.Services;

namespace ZorgmaatjeAPI.Tests.ServiceTests
{
    [TestClass]
    public class AuthorizationServiceTests
    {
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private AuthorizationService _authorizationService;

        [TestInitialize]
        public void Setup()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var connectionStringService = new ConnectionStringService("Server=(localdb)\\mssqllocaldb;Database=TestDatabase;Trusted_Connection=True;");
            _authorizationService = new AuthorizationService(_mockHttpContextAccessor.Object, connectionStringService);
        }

        [TestMethod]
        public async Task IsUserAuthorizedForEntityAsync_ShouldReturnFalse_WhenUserIdIsNull()
        {
            // Arrange
            var entityId = "test-entity-id";
            var entityTable = "Child";
            var httpContext = new DefaultHttpContext { User = new ClaimsPrincipal() };

            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

            // Act
            var result = await _authorizationService.IsUserAuthorizedForEntityAsync(entityTable, entityId);

            // Assert
            Assert.IsFalse(result);
        }
    }
}


