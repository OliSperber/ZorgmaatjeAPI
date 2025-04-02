using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ZorgmaatjeAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace ZorgmaatjeAPI.Tests.ServiceTests;


[TestClass]
public class TokenServiceTests
{
    private TokenService _tokenService;

    [TestInitialize]
    public void Setup()
    {
        var configuration = new Mock<IConfiguration>();
        configuration.SetupGet(x => x["SecretJwtKey"]).Returns("your_longer_secret_key_here_12345");
        configuration.SetupGet(x => x["Jwt:Issuer"]).Returns("your_issuer_here");
        configuration.SetupGet(x => x["Jwt:Audience"]).Returns("your_audience_here");

        _tokenService = new TokenService(configuration.Object);
    }

    [TestMethod]
    public void GenerateJwtToken_ShouldSetCorrectClaims()
    {
        // Arrange
        var user = new IdentityUser
        {
            Id = "test-user-id",
            UserName = "testuser",
            Email = "testuser@example.com"
        };

        // Act
        var token = _tokenService.GenerateJwtToken(user);
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        // Assert
        var claims = jwtToken.Claims
            .Where(c => c.Type == ClaimTypes.NameIdentifier || c.Type == ClaimTypes.Name || c.Type == ClaimTypes.Email)
            .ToList();

        Assert.IsNotNull(jwtToken);
        Assert.AreEqual("your_issuer_here", jwtToken.Issuer);
        Assert.AreEqual("your_audience_here", jwtToken.Audiences.First());

        Assert.AreEqual(3, claims.Count);
        Assert.AreEqual("test-user-id", claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
        Assert.AreEqual("testuser", claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value);
        Assert.AreEqual("testuser@example.com", claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value);
    }
}



