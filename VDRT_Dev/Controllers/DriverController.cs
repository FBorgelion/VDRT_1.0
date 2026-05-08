using BL.DTOs.Driver;
using BL.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace VDRT_Dev_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {

        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DriverDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var drivers = await _driverService.GetAllAsync();
                if (drivers.Count() == 0)
                    return NoContent();
                return Ok(drivers);
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
                var driver = await _driverService.GetByIdAsync(id);
                if (driver == null)
                    return NotFound("No driver has been found.");
                return Ok(driver);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateDriverDto dto)
        {
            try
            {
                var chauffeur = await _driverService.AddAsync(dto);

                return StatusCode(StatusCodes.Status201Created, chauffeur);
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
        public async Task<IActionResult> Update(int id, UpdateDriverDto dto)
        {
            try
            {
                var result = await _driverService.UpdateAsync(id, dto);
                if (!result)
                    return NotFound("No driver has been found.");
                return Ok("Driver updated successfully.");
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
                var result = await _driverService.DeleteAsync(id);
                if (!result)
                    return NotFound("No driver has been found.");
                return Ok("Driver deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
