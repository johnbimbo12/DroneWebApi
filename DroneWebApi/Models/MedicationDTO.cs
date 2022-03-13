using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DroneWebApi.Models
{
    public class MedicationDTO : LoadMedicationDTO
    {
        public int Id { get; set; }
        //public DroneDTO Drone { get; set; }
    }

    public class LoadMedicationDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        public string Code { get; set; }

        //[Required]
        public string Image { get; set; }

        [Required]
        public int DroneId { get; set; }
    }
}
