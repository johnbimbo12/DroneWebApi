using DroneWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneWebApi.Configurations.Entities
{
    public class DroneConfiguration : IEntityTypeConfiguration<Drone>
    {
        public void Configure(EntityTypeBuilder<Drone> builder)
        {
            builder.HasData(
                new Drone
                {
                    Id = 1,
                    SerialNumber = "DR001",
                    Model = Enums.Model.Cruiserweight,
                    WeightLimit = 450,
                    BatteryCapacity = 90,
                    State = Enums.State.IDLE
                },
                new Drone
                {
                    Id = 2,
                    SerialNumber = "DR002",
                    Model = Enums.Model.Heavyweight,
                    WeightLimit = 400,
                    BatteryCapacity = 95,
                    State = Enums.State.RETURNING
                },
                new Drone
                {
                    Id = 3,
                    SerialNumber = "DR003",
                    Model = Enums.Model.Lightweight,
                    WeightLimit = 300,
                    BatteryCapacity = 80,
                    State = Enums.State.LOADED
                },
                new Drone
                {
                    Id = 4,
                    SerialNumber = "DR004",
                    Model = Enums.Model.Middleweight,
                    WeightLimit = 320,
                    BatteryCapacity = 92,
                    State = Enums.State.DELIVERED
                },
                new Drone
                {
                    Id = 5,
                    SerialNumber = "DR005",
                    Model = Enums.Model.Middleweight,
                    WeightLimit = 280,
                    BatteryCapacity = 90,
                    State = Enums.State.DELIVERING
                },
                new Drone
                {
                    Id = 6,
                    SerialNumber = "DR006",
                    Model = Enums.Model.Heavyweight,
                    WeightLimit = 440,
                    BatteryCapacity = 98,
                    State = Enums.State.LOADED
                },
                new Drone
                {
                    Id = 7,
                    SerialNumber = "DR007",
                    Model = Enums.Model.Cruiserweight,
                    WeightLimit = 500,
                    BatteryCapacity = 85,
                    State = Enums.State.IDLE
                }

            );

        }
    }
}
