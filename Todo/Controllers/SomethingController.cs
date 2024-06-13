using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ToDo.Data;
using ToDo.Dtos;
using ToDo.Interfaces;
using ToDo.Models;

namespace ToDo.Controllers
{
    [Route("BaiTap/ToDo")]
    [ApiController]
    public class SomethingController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ISomethingRepository _somethingRepo;

        public SomethingController(ApplicationDBContext context, ISomethingRepository somethingRepo)
        {
            _context = context;
            _somethingRepo = somethingRepo;
        }

        [EnableCors("AllowSpecificOrigin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var somethings = await _somethingRepo.GetAllAsync();
            return Ok(somethings);
        }
        [EnableCors("AllowSpecificOrigin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var something = await _somethingRepo.GetByIdAsync(id);
            if (something == null)
            {
                return NotFound();
            }
            return Ok(something);
        }
        [EnableCors("AllowSpecificOrigin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSomethingRequestDto somethingRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var somethingModel = new Something
            {
                Somethings=somethingRequestDto.Somethings
            };
            var createdSomething = await _somethingRepo.CreateAsync(somethingModel);
            return CreatedAtAction(nameof(GetById), new {id=createdSomething.Id}, createdSomething);
        }
        [EnableCors("AllowSpecificOrigin")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var somethingModel = await _somethingRepo.DeleteAsync(id);
            if(somethingModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
