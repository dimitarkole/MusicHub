namespace MusicHub.Services.Data.Tests.ClassFixtures
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels;

    public class MappingsProvider
    {
        public MappingsProvider()
        {
            //Register all mappings in the app
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
        }
    }
}
