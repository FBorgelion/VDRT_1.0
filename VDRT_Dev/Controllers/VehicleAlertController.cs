using BL.DTOs.Driver;
using BL.DTOs.VehicleAlert;
using BL.Interfaces.Services;
using BL.Services;
using Microsoft.AspNetCore.Mvc;

namespace VDRT_Dev_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleAlertController : ControllerBase
    {
        private readonly IVehicleAlertService _vehicleAlertService;

        public VehicleAlertController(IVehicleAlertService vehicleAlertService)
        {
            _vehicleAlertService = vehicleAlertService;
        }

         [HttpGet]
         [ProducesResponseType(typeof(IEnumerable<VehicleAlertDto>), StatusCodes.Status200OK)]
         [ProducesResponseType(StatusCodes.Status500InternalServerError)]
         [ProducesResponseType(StatusCodes.Status204NoContent)]
         public async Task<IActionResult> GetAll()
         {
             try
             {
                 var vehicleAlerts = await _vehicleAlertService.GetAllAsync();
                 if (vehicleAlerts.Count() == 0)
                     return NoContent();
                 return Ok(vehicleAlerts);
             }
             catch (Exception ex)
             {
                 return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
             }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var alert = await _vehicleAlertService.GetByIdAsync(id);
                if (alert == null)
                    return NotFound("No alert has been found.");
                return Ok(alert);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateVehicleAlertDto dto)
        {
            try
            {
                var alert = await _vehicleAlertService.AddAsync(dto);

                return StatusCode(StatusCodes.Status201Created, alert);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, UpdateVehicleAlertDto dto)
        {
            try
            {
                var result = await _vehicleAlertService.UpdateAsync(id, dto);
                if (!result)
                    return NotFound("No alert has been found.");
                return Ok("alert updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _vehicleAlertService.DeleteAsync(id);
                if (!result)
                    return NotFound("No alert has been found.");
                return Ok("alert deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
