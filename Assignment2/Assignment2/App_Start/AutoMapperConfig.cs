using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment2
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

            //Get data from user and send to user 
            Mapper.CreateMap<Controllers.EmployeeAdd, Models.Employee>();

            //Get data from database and send data to view
            Mapper.CreateMap<Models.Employee, Controllers.EmployeeBase>();

#pragma warning restore CS0618
        }
    }
}