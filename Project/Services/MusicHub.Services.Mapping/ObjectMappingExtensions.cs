using System;
using AutoMapper;
namespace MusicHub.Services.Mapping
{
    public static class ObjectMappingExtensions
    {
        public static T To<T>(this object origin)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }

            return Mapper.Map<T>(origin);
        }

        public static Destination To<Destination>(this object source, object destination) =>
        (Destination) Mapper.Map(source, destination, source.GetType(), destination.GetType());

        public static Destination To<Source, Destination>(this Source source, Destination destination, Action<IMappingOperationOptions<Source, Destination>> options) =>
           Mapper.Map(source, destination, options);
    }
}