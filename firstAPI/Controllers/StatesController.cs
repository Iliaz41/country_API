using AutoMapper;
using firstAPI.DTO.States;
using firstAPI.Models;
using firstAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace firstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly IStatesRepository _statesRepository;
        private readonly IMapper _mapper;

        public StatesController(IStatesRepository statesRepository, IMapper mapper)
        {
            _statesRepository = statesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StatesDto>>> GetAll()
        {
            var countries = await _statesRepository.GetAll();
            var countriesDto = _mapper.Map<List<StatesDto>>(countries);
            if (countries == null)
            {
                return NoContent();
            }
            return Ok(countriesDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<StatesDto>> GetById(int id)
        {
            var states = await _statesRepository.GetById(id);
            var statesDto = _mapper.Map<StatesDto>(states);
            if (states == null)
            {
                return NoContent();
            }
            return Ok(statesDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateStatesDto>> Create([FromBody] CreateStatesDto statesDto)
        {
            var result = _statesRepository.IsStateExsits(statesDto.Name);
            if (result)
            {
                return Conflict("States already exsits in databse");
            }
            var states = _mapper.Map<States>(statesDto);
            await _statesRepository.Create(states);
            return CreatedAtAction("GetById", new { id = states.Id }, states);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<States>> Update(int id, [FromBody] UpdateStatesDto statesDto)
        {
            if (statesDto == null || id != statesDto.Id)
            {
                return BadRequest();
            }
            var states = _mapper.Map<States>(statesDto);
            await _statesRepository.Update(states);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var states = await _statesRepository.GetById(id);
            if (states == null)
            {
                return NotFound();
            }
            await _statesRepository.Delete(states);
            return NoContent();
        }
    }
}
