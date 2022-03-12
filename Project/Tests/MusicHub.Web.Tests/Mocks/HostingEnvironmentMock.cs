using System;
using System.IO;

using Microsoft.AspNetCore.Hosting;
using Moq;

namespace MusicHub.Web.Tests.Mocks
{
    public class HostingEnvironmentMock
    {
        private static readonly string WebRootPath = Path.Combine(Environment.CurrentDirectory, "../../../../../Web/MusicHub.Web/client/src/assets/resources");

        public static IHostingEnvironment CreateInstance()
            => CreateInstance(WebRootPath);

        public static IHostingEnvironment CreateInstance(string webRootPath)
        {
            var hostingEnvironmentMock = new Mock<IHostingEnvironment>();
            hostingEnvironmentMock.Setup(h => h.WebRootPath).Returns(webRootPath);

            return hostingEnvironmentMock.Object;
        }
    }
}
