using MusicHub.Web.Tests.TestData.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System.Security.Claims;

namespace MusicHub.Web.Tests.Infrastructure.Helpers
{
    public class JwtTokenHelperTests
    {
       /* private readonly JwtHвeader jwtTokenHelper;
        private readonly ApplicationSettings appSettings;

        public JwtTokenHelperTests()
        {
            appSettings = FakeAppSettings.GetAppSettings();
            jwtTokenHelper = new JwtHelper(appSettings);
        }

    [Fact]
    public void GenerateToken_WithValidParametrs_ShouldReturnValidToken()
    {
        // Arrange
        var claims = new List<Claim>
            {
                new Claim("test", "value")
            };

        // Act
        string token = jwtTokenHelper.GenerateToken(appSettings.FileTokenKey, DateTime.UtcNow.AddDays(1), claims, appSettings.SiteUrl, appSettings.SiteUrl);

        // Assert
        Assert.NotNull(token);
    }

    [Fact]
    public void ValidateToken_WithValidToken_ShouldReturnTrue()
    {
        // Arrange
        string token = jwtTokenHelper.GenerateToken(appSettings.FileTokenKey, DateTime.UtcNow.AddDays(1), new List<Claim>(), appSettings.SiteUrl, appSettings.SiteUrl);

        // Act
        bool isValid = jwtTokenHelper.ValidateToken(token, appSettings.FileTokenKey, appSettings.SiteUrl, appSettings.SiteUrl);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void ValidateToken_WithInvalidToken_ShouldReturnFalse()
    {
        // Arrange
        string token = jwtTokenHelper.GenerateToken(appSettings.FileTokenKey, DateTime.UtcNow.AddDays(1), new List<Claim>(), appSettings.SiteUrl, appSettings.SiteUrl);

        // Act
        bool isValid = jwtTokenHelper.ValidateToken(token + "wrong", appSettings.FileTokenKey, appSettings.SiteUrl, appSettings.SiteUrl);

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void ValidateToken_WithInvalidKey_ShouldReturnFalse()
    {
        // Arrange
        string token = jwtTokenHelper.GenerateToken(appSettings.FileTokenKey, DateTime.UtcNow.AddDays(1), new List<Claim>(), appSettings.SiteUrl, appSettings.SiteUrl);

        // Act
        bool isValid = jwtTokenHelper.ValidateToken(token, "wrong key", appSettings.SiteUrl, appSettings.SiteUrl);

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void ValidateToken_WithInvalidIssuerAndAudiance_ShouldReturnFalse()
    {
        // Arrange
        string token = jwtTokenHelper.GenerateToken(appSettings.FileTokenKey, DateTime.UtcNow.AddDays(1), new List<Claim>(), appSettings.SiteUrl, appSettings.SiteUrl);

        // Act
        bool isValid = jwtTokenHelper.ValidateToken(token, "wrong key", appSettings.SiteUrl, appSettings.SiteUrl);

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void ValidateFileAccessToken_WithValidToken_ShouldReturnTrue()
    {
        // Arrange
        int fileId = 1;
        string token = jwtTokenHelper.GenerateFileToken(fileId);

        // Act
        bool isValid = jwtTokenHelper.IsValidFileToken(token, fileId);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void ValidateFileAccessToken_WithInvalidFileId_ShouldReturnFalse()
    {
        // Arrange
        int fileId = 1;
        string token = jwtTokenHelper.GenerateFileToken(fileId);

        // Act
        bool isValid = jwtTokenHelper.IsValidFileToken(token, 10);

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void ValidateFileAccessToken_WithInvalidToken_ShouldReturnFalse()
    {
        // Arrange
        int fileId = 1;
        string token = jwtTokenHelper.GenerateFileToken(fileId);

        // Act
        bool isValid = jwtTokenHelper.IsValidFileToken(token + "y", fileId);

        // Assert
        Assert.False(isValid);
    }*/
    }
}
