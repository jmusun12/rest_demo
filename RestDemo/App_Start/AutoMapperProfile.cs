using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using RestDemo.Models;
using RestDemo.DTOs;

namespace RestDemo.App_Start
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmpleadoInfo, EmpleadoDto>().ReverseMap();
            CreateMap<EmpleadoInfo, DetalleEmpleadoDto>().ReverseMap();
        }
    }
}