using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment8
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Disable AutoMapper v4.2.x warnings
#pragma warning disable CS0618

            // Attention - AutoMapper create map statements
            // Mapper.CreateMap< FROM , TO >();
            // Add map creation statements here


            Mapper.CreateMap<Models.Artist, Controllers.ArtistBase>();
            Mapper.CreateMap<Models.Artist, Controllers.ArtistWithDetail>();

            Mapper.CreateMap<Models.Album, Controllers.AlbumBase>();
            Mapper.CreateMap<Models.Album, Controllers.AlbumWithDetail>();

            Mapper.CreateMap<Models.Track, Controllers.TrackBase>();
            Mapper.CreateMap<Models.Track, Controllers.TrackWithDetail>();

            Mapper.CreateMap<Controllers.ArtistAdd, Models.Artist>();
            Mapper.CreateMap<Controllers.AlbumAdd, Models.Album>();
            Mapper.CreateMap<Controllers.TrackAdd, Models.Track>();

            Mapper.CreateMap<Models.Genre, Controllers.GenreBase>();

            //.. add more 


#pragma warning restore CS0618
        }
    }
}