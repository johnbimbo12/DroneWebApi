using DroneWebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneWebApi.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Drone> Drones { get; }
        IGenericRepository<Medication> Medications { get; }
        Task Save();
    }
}
