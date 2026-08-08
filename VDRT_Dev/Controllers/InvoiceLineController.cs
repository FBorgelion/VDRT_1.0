using BL.DTOs.InvoiceLine;
using BL.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace VDRT_Dev_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceLineController : ControllerBase
    {

        private readonly IInvoiceLineService _invoiceLineService;

        public InvoiceLineController(IInvoiceLineService invoiceLineService)
        {
            _invoiceLineService = invoiceLineService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<InvoiceLineDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var lines = await _invoiceLineService.GetAllAsync();
                if (lines.Count() == 0)
                    return NoContent();
                return Ok(lines);
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
                var line = await _invoiceLineService.GetByIdAsync(id);
                if (line == null)
                    return NotFound("No line has been found.");
                return Ok(line);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateInvoiceLineDto dto)
        {
            try
            {
                var line = await _invoiceLineService.AddAsync(dto);

                return StatusCode(StatusCodes.Status201Created, line);
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
        public async Task<IActionResult> Update(int id, UpdateInvoiceLineDto dto)
        {
            try
            {
                var result = await _invoiceLineService.UpdateAsync(id, dto);
                if (!result)
                    return NotFound("No line has been found.");
                return Ok("Line updated successfully.");
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
                var result = await _invoiceLineService.DeleteAsync(id);
                if (!result)
                    return NotFound("No line has been found.");
                return Ok("Line deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
