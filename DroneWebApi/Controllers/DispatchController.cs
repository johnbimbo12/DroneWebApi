using AutoMapper;
using DroneWebApi.Data;
using DroneWebApi.IRepository;
using DroneWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DispatchController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DispatchController> _logger;
        private readonly IMapper _mapper;

        public DispatchController(IUnitOfWork unitOfWork, ILogger<DispatchController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDrones()
        {
            var drones = await _unitOfWork.Drones.GetAll();
            var results = _mapper.Map<IList<DroneDTO>>(drones);
            return Ok(results);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMedications()
        {
            var medications = await _unitOfWork.Medications.GetAll();
            var results = _mapper.Map<IList<MedicationDTO>>(medications);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetDrone")]
        public async Task<IActionResult> GetDrone(int id)
        {
            var drone = await _unitOfWork.Drones.Get(q => q.Id == id, new List<string> { "Medications" });
            var result = _mapper.Map<DroneWithMedicationsDTO>(drone);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterDrone([FromBody] CreateDroneDTO droneDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(RegisterDrone)}");
                return BadRequest(ModelState);
            }
            var drone = _mapper.Map<Drone>(droneDTO);
            await _unitOfWork.Drones.Insert(drone);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetDrone", new { id = drone.Id }, drone);
        }

        [HttpPost]
        public async Task<IActionResult> LoadMedication([FromBody] CreateMedicationDTO medicationDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(LoadMedication)}");
                return BadRequest(ModelState);
            }
            var medication = _mapper.Map<Medication>(medicationDTO);
            await _unitOfWork.Medications.Insert(medication);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetMedication", new { id = medication.Id }, medication);
        }

        [HttpGet("{id:int}",Name = "GetMedication")]
        public async Task<IActionResult> GetMedication(int id)
        {
            var medication = await _unitOfWork.Medications.Get(q => q.Id == id);
            var result = _mapper.Map<MedicationDTO>(medication);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetIdleDrones()
        {
            var drones = await _unitOfWork.Drones.GetAll(q => q.State == 0);
            var results = _mapper.Map<IList<DroneDTO>>(drones);
            return Ok(results);
        }
    }
}
















































































































