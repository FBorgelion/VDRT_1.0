using BL.DTOs.Driver;
using BL.DTOs.Timesheet;
using BL.Interfaces.Services;
using BL.Services;
using Microsoft.AspNetCore.Mvc;

namespace VDRT_Dev_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimesheetController : ControllerBase
    {
        private readonly ITimesheetService _timesheetService;

        public TimesheetController(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TimesheetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var timesheets = await _timesheetService.GetAllAsync();
                if (timesheets.Count() == 0)
                    return NoContent();
                return Ok(timesheets);
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
                var timesheet = await _timesheetService.GetByIdAsync(id);
                if (timesheet == null)
                    return NotFound("No timesheet has been found.");
                return Ok(timesheet);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateTimesheetDto dto)
        {
            try
            {
                var timesheet = await _timesheetService.AddAsync(dto);

                return StatusCode(StatusCodes.Status201Created, timesheet);
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
        public async Task<IActionResult> Update(int id, UpdateTimesheetDto dto)
        {
            try
            {
                var result = await _timesheetService.UpdateAsync(id, dto);
                if (!result)
                    return NotFound("No timesheet has been found.");
                return Ok("Timesheet updated successfully.");
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
                var result = await _timesheetService.DeleteAsync(id);
                if (!result)
                    return NotFound("No timesheet has been found.");
                return Ok("Timesheet deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
