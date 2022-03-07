using DroneWebApi.Data;
using DroneWebApi.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneWebApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IGenericRepository<Drone> _drones;
        private IGenericRepository<Medication> _medications;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IGenericRepository<Drone> Drones => _drones ??= new GenericRepository<Drone>(_context);

        public IGenericRepository<Medication> Medications => _medications ??= new GenericRepository<Medication>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
