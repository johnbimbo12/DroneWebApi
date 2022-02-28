using AutoMapper;
using DroneWebApi.Data;
using DroneWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneWebApi.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Drone, CreateDroneDTO>().ReverseMap();
            CreateMap<Drone, DroneDTO>().ReverseMap();
        }
    }
}
