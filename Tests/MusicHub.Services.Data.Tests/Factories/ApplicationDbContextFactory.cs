namespace MusicHub.Services.Data.Tests.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContextFactory
    {
        public static ApplicationDbContext CreateInMemoryDatabase()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
