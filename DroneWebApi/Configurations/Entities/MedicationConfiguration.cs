using DroneWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneWebApi.Configurations.Entities
{
    public class MedicationConfiguration : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder.HasData(
                new Medication
                {
                    Id = 1,
                    Code = "MD_PCM_TB",
                    Name = "Paracetamol Tablets",
                    Weight = 300,
                    DroneId = 5
                },
                new Medication
                {
                    Id = 2,
                    Code = "MD_CL_IV",
                    Name = "Chloroquine Intravenous",
                    Weight = 350,
                    DroneId = 6
                },
                new Medication
                {
                    Id = 3,
                    Code = "MD_MET_TB",
                    Name = "Metronidazole Tablets",
                    Weight = 200,
                    DroneId = 5
                },
                new Medication
                {
                    Id = 4,
                    Code = "MD_VITC_SY",
                    Name = "Vitamin C Syrup",
                    Weight = 375,
                    DroneId = 3
                }
            );
        }
    }
}
