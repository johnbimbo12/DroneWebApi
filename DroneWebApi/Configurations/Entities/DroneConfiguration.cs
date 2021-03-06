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
                    Model = Model.Cruiserweight,
                    WeightLimit = 450,
                    BatteryCapacity = 90,
                    State = State.IDLE
                },
                new Drone
                {
                    Id = 2,
                    SerialNumber = "DR002",
                    Model = Model.Heavyweight,
                    WeightLimit = 400,
                    BatteryCapacity = 95,
                    State = State.RETURNING
                },
                new Drone
                {
                    Id = 3,
                    SerialNumber = "DR003",
                    Model = Model.Lightweight,
                    WeightLimit = 300,
                    BatteryCapacity = 80,
                    State = State.LOADED
                },
                new Drone
                {
                    Id = 4,
                    SerialNumber = "DR004",
                    Model = Model.Middleweight,
                    WeightLimit = 320,
                    BatteryCapacity = 92,
                    State = State.DELIVERED
                },
                new Drone
                {
                    Id = 5,
                    SerialNumber = "DR005",
                    Model = Model.Middleweight,
                    WeightLimit = 280,
                    BatteryCapacity = 90,
                    State = State.DELIVERING
                },
                new Drone
                {
                    Id = 6,
                    SerialNumber = "DR006",
                    Model = Model.Heavyweight,
                    WeightLimit = 440,
                    BatteryCapacity = 98,
                    State = State.LOADED
                },
                new Drone
                {
                    Id = 7,
                    SerialNumber = "DR007",
                    Model = Model.Cruiserweight,
                    WeightLimit = 500,
                    BatteryCapacity = 85,
                    State = State.IDLE
                }

            );

        }
    }
}
