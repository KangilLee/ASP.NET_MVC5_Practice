using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment4
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

            Mapper.CreateMap<Models.Invoice, Controllers.InvoiceBase>();

            Mapper.CreateMap<Models.Invoice, Controllers.InvoiceWithDetail>();

            Mapper.CreateMap<Models.InvoiceLine, Controllers.InvoiceLineBase>();

            Mapper.CreateMap<Models.InvoiceLine, Controllers.InvoiceLineWithDetail>();



#pragma warning restore CS0618
        }
    }
}