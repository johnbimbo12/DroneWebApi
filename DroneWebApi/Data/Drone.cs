using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneWebApi.Data
{
    public class Drone
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public double WeightLimit { get; set; }
        public double BatteryCapacity { get; set; }
        private string _state { get; set; } = "IDLE";

        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }
        public virtual IList<Medication> Medications { get; set; }
    }
}
