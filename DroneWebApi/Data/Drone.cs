using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DroneWebApi.Data.Enums;

namespace DroneWebApi.Data
{
    public class Drone
    {
        public string SerialNumber { get; set; }
        public Model Model { get; set; }
        public double WeightLimit { get; set; }
        public double BatteryCapacity { get; set; }
        public State State { get; set; }
    }
}
