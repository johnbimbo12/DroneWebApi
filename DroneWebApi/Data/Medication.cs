using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DroneWebApi.Data
{
    public class Medication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Code { get; set; }
        public string Image { get; set; }

        [ForeignKey(nameof(Drone))]
        public int DroneId { get; set; }
        //public Drone Drone { get; set; }
    }
}
