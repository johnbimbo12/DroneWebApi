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
        [RegularExpression(@"^[A-Za-z0-9_-]+$", ErrorMessage = "Only letters, numbers, hyphens and underscores are allowed")]
        public string Name { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z0-9_]+$", ErrorMessage = "Only uppercase letters, numbers and underscores are allowed")]
        public string Code { get; set; }

        //[Required]
        public string Image { get; set; }

        [Required]
        public int DroneId { get; set; }
    }
}
