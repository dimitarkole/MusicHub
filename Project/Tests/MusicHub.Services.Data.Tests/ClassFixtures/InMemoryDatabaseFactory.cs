namespace MusicHub.Services.Data.Tests.ClassFixtures
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data;
    using MusicHub.Services.Data.Tests.Factories;

    public class InMemoryDatabaseFactory
    {
        public ApplicationDbContext Context { get; private set; }

        public InMemoryDatabaseFactory()
        {
            Context = ApplicationDbContextFactory.CreateInMemoryDatabase();
        }

        public void Dispose() => Context.Dispose();
    }
}
