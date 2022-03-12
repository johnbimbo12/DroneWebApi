using DroneWebApi.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DroneWebApi.Models
{
    public class DroneWithMedicationsDTO : DroneDTO
    {
        public IList<Medication> Medications { get; set; }
    }

    public class DroneDTO : CreateDroneDTO
    {
        public int Id { get; set; }
    }

    public class CreateDroneDTO
    {
        [Required]
        [StringLength(maximumLength:100, ErrorMessage = "Serial Number is too long")]
        public string SerialNumber { get; set; }

        [Required]
        public string Model { get; set; }
        
        [Required]
        [Range(1,500)]
        public double WeightLimit { get; set; }

        [Required]
        [Range(1,100)]
        public double BatteryCapacity { get; set; }        
    }

}
