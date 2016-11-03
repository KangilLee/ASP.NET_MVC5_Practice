using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment_3
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

            //For getting data
            Mapper.CreateMap<Models.Employee,Controllers.EmployeeBase>();

            //For getting data
            Mapper.CreateMap<Models.Track,Controllers.TrackBase>();

            //For editing data from list page to edit
            Mapper.CreateMap<Controllers.EmployeeBase, Controllers.EmployeeEditContactInfoForm>();

#pragma warning restore CS0618
        }
    }
}