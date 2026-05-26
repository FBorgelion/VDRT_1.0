using BL.DTOs.TripAnomaly;
using BL.Interfaces.Services;
using BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VDRT_Dev_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripAnomalyController : ControllerBase
    {

        private readonly ITripAnomalyService _tripAnomalyService;

        public TripAnomalyController(ITripAnomalyService tripAnomalyService)
        {
            _tripAnomalyService = tripAnomalyService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TripAnomalyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var anomalies = await _tripAnomalyService.GetAllAsync();
                if (anomalies.Count() == 0)
                    return NoContent();
                return Ok(anomalies);
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
                var anomaly = await _tripAnomalyService.GetByIdAsync(id);
                if (anomaly == null)
                    return NotFound("No anomaly has been found.");
                return Ok(anomaly);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateTripAnomalyDto dto)
        {
            try
            {
                var anomaly = await _tripAnomalyService.AddAsync(dto);

                return StatusCode(StatusCodes.Status201Created, anomaly);
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
        public async Task<IActionResult> Update(int id, UpdateTripAnomalyDto dto)
        {
            try
            {
                var result = await _tripAnomalyService.UpdateAsync(id, dto);
                if (!result)
                    return NotFound("No anomaly has been found.");
                return Ok("Anomaly updated successfully.");
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
                var result = await _tripAnomalyService.DeleteAsync(id);
                if (!result)
                    return NotFound("No anomaly has been found.");
                return Ok("Anomaly deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
