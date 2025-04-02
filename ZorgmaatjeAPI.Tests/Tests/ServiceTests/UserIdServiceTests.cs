using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ZorgmaatjeAPI.Services;

namespace ZorgmaatjeAPI.Tests.ServiceTests;

[TestClass]
public class UserIdServiceTests
{
    private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private UserIdService _userIdService;

    [TestInitialize]
    public void Setup()
    {
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _userIdService = new UserIdService(_mockHttpContextAccessor.Object);
    }

    [TestMethod]
    public void GetUserIdFromToken_ShouldReturnUserId()
    {
        // Arrange
        var userId = "test-user-id";
        var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        var httpContext = new DefaultHttpContext { User = claimsPrincipal };

        _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

        // Act
        var result = _userIdService.GetUserIdFromToken();

        // Assert
        Assert.AreEqual(userId, result);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "userId not found in token")]
    public void GetUserIdFromToken_ShouldThrowException_WhenUserIdNotFound()
    {
        // Arrange
        var httpContext = new DefaultHttpContext { User = new ClaimsPrincipal() };
        _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

        // Act
        _userIdService.GetUserIdFromToken();

        // Assert is handled by ExpectedException
    }
}


