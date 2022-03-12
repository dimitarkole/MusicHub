namespace MusicHub.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data;
    using MusicHub.Services.Data.Tests.ClassFixtures;
    using MusicHub.Services.Data.Tests.Factories;
    using Xunit;

    public class BaseTestClass : IClassFixture<MappingsProvider>
    {
        protected readonly ApplicationDbContext context;

        public BaseTestClass()
        {
            this.context = ApplicationDbContextFactory.CreateInMemoryDatabase();
        }
    }
}
