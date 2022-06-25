using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;


namespace RestDemo.App_Start
{
    public class AutoMapperConfig
    {
        public static IMapper GetMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            return config.CreateMapper();

        }
    }
}