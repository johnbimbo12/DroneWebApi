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
            if (droneModel.Equals("Invalid Model"))
                return BadRequest("Drone model unknown");
            
            string modelName = droneModel.ToString();
            drone.Model = modelName;
            await _unitOfWork.Drones.Insert(drone);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetDrone", new { id = drone.Id }, drone);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoadMedication([FromBody] IList<CreateMedicationDTO> medicationDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(LoadMedication)}");
                return BadRequest(ModelState);
            }

            var drone = await _unitOfWork.Drones.Get(q => q.Id == medicationDTO.FirstOrDefault().DroneId, new List<string> { "Medications" });
            if(drone == null)
            {
                return BadRequest("Invalid DroneID submitted");
            }

            //drone should only be allowed to load if in Idle or Loading State
            if(drone.State != State.IDLE && drone.State != State.LOADING)
            {
                return BadRequest($"Mission impossible. Drone is currently in {drone.State} state.");
            }

            //drone cannot load if battery is less than 25%
            if(drone.BatteryCapacity < 25)
            {
                return BadRequest($"Mission impossible. Battery percentage is {drone.BatteryCapacity}%.");
            }

            var currentDroneCapacity = drone.Medications.Count > 0 ? drone.Medications.Sum(q => q.Weight) : 0;
            _logger.LogInformation($"Current Drone Loaded Weight is {currentDroneCapacity}gr");

            //If free space on the drone is less than the sum of the weight of medication items to be loaded, throw an error
            if ((drone.WeightLimit - currentDroneCapacity) < medicationDTO.Sum(q => q.Weight))
            {
                return BadRequest($"Weight limit for drone exceeded. Maximum: {drone.WeightLimit}gr, Current: {currentDroneCapacity}gr");
            }

            //update the current drone capacity
            currentDroneCapacity += medicationDTO.Sum(q => q.Weight);

            //this sets the state of the drone to loaded once it reaches its capacity or loading if there's still space left
            if ((drone.State == State.IDLE || drone.State == State.LOADING) && drone.WeightLimit - currentDroneCapacity > 0)
                drone.State = State.LOADING;
            else if ((drone.State == State.IDLE || drone.State == State.LOADING) && drone.WeightLimit - currentDroneCapacity == 0)
                drone.State = State.LOADED;

            var medication = _mapper.Map<IList<Medication>>(medicationDTO);
            await _unitOfWork.Medications.InsertRange(medication);
            _unitOfWork.Drones.Update(drone);
            await _unitOfWork.Save();

            if (medicationDTO.Count == 1)
                return CreatedAtRoute("GetMedication", new { id = medication.FirstOrDefault().Id }, medication);
            else
                return Ok(medicationDTO);
        }

        [HttpGet("{id:int}",Name = "GetMedication")]
        public async Task<IActionResult> GetMedication(int id)
        {
            var medication = await _unitOfWork.Medications.Get(q => q.Id == id);
            var result = _mapper.Map<MedicationDTO>(medication);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableDrones()
        {
            var drones = await _unitOfWork.Drones.GetAll(q => q.State == State.IDLE || q.State == State.LOADING);
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
        
        string GetModel(string model)
        {
            if (model.ToLower() == Model.Cruiserweight.ToLower())
                return Model.Cruiserweight;

            else if (model.ToLower() == Model.Heavyweight.ToLower())
                return Model.Heavyweight;

            else if (model.ToLower() == Model.Lightweight.ToLower())
                return Model.Lightweight;

            else if (model.ToLower() == Model.Middleweight.ToLower())
                return Model.Middleweight;

            return "Invalid Model";
        }
    }
}
















































































































