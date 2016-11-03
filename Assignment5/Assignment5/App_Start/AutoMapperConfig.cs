using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment5
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Disable AutoMapper v4.2.x warnings
#pragma warning disable CS0618

            // AutoMapper create map statements
            // Mapper.CreateMap< FROM , TO >();
            // Add map creation statements here

            Mapper.CreateMap<Models.Track, Controllers.TrackBase>();

            Mapper.CreateMap<Models.Track, Controllers.TrackWithDetail>();

            Mapper.CreateMap<Models.Album, Controllers.AlbumBase>();

            Mapper.CreateMap<Models.MediaType, Controllers.MediaTypeBase>();

            Mapper.CreateMap<Controllers.TrackAdd, Models.Track>();


#pragma warning restore CS0618
        }
    }
}