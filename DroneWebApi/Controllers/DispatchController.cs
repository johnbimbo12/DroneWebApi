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
            if(drones == null || drones.Count == 0)
            {
                return StatusCode(500, "No drones found.");
            }
            var results = _mapper.Map<IList<DroneDTO>>(drones);
            return Ok(results);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMedications()
        {
            var medications = await _unitOfWork.Medications.GetAll();
            if(medications == null || medications.Count == 0)
            {
                return StatusCode(500, "No medications found.");
            }
            var results = _mapper.Map<IList<MedicationDTO>>(medications);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetDrone")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDrone(int id)
        {
            var drone = await _unitOfWork.Drones.Get(q => q.Id == id, new List<string> { "Medications" });
            if (drone == null)
            {
                return BadRequest($"Drone with ID {id} not found.");
            }
            var result = _mapper.Map<DroneWithMedicationsDTO>(drone);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterDrone([FromBody] CreateDroneDTO droneDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(RegisterDrone)}");
                return BadRequest(ModelState);
            }
            var drone = _mapper.Map<Drone>(droneDTO);
            var droneModel = GetModel(drone.Model);
            if (droneModel.Equals(StatusCode(200)))
                return BadRequest("Model unknown");
            
            string modelName = droneModel.ToString();
            drone.Model = modelName;
            await _unitOfWork.Drones.Insert(drone);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetDrone", new { id = drone.Id }, drone);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoadMedication([FromBody] CreateMedicationDTO medicationDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(LoadMedication)}");
                return BadRequest(ModelState);
            }

            var drone = await _unitOfWork.Drones.Get(q => q.Id == medicationDTO.DroneId);
            if(drone == null)
            {
                return BadRequest("Invalid DroneID submitted");
            }

            if (drone.WeightLimit < medicationDTO.Weight)
            {
                return BadRequest($"Weight limit for drone exceeded. Maximum: {drone.WeightLimit}gr");
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
            var drones = await _unitOfWork.Drones.GetAll(q => q.State == State.IDLE);
            var results = _mapper.Map<IList<DroneDTO>>(drones);
            return Ok(results);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDroneBatteryLevel(int id)
        {
            var drone = await _unitOfWork.Drones.Get(q => q.Id == id);
            var result = _mapper.Map<DroneDTO>(drone);
            return Ok(result.BatteryCapacity);
        }

        IActionResult GetState(string state)
        {
            if (state == State.DELIVERED)
                return Ok(State.DELIVERED);
            else if (state == State.DELIVERING)
                return Ok(state);
            else if (state == State.IDLE)
                return Ok(state);
            else if (state == State.LOADED)
                return Ok(state);
            else if (state == State.LOADING)
                return Ok(state);
            else if (state == State.RETURNING)
                return Ok(state);
            return BadRequest("Invalid State");
        }

        IActionResult GetModel(string model)
        {
            if (model.ToLower() == Model.Cruiserweight.ToLower())
                return Ok(Model.Cruiserweight);

            else if (model.ToLower() == Model.Heavyweight.ToLower())
                return Ok(Model.Heavyweight);

            else if (model.ToLower() == Model.Lightweight.ToLower())
                return Ok(Model.Lightweight);

            else if (model.ToLower() == Model.Middleweight.ToLower())
                return Ok(Model.Middleweight);

            return BadRequest("Invalid Model");
        }
    }
}
















































































































