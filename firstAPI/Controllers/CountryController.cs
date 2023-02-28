using AutoMapper;
using firstAPI.Data;
using firstAPI.DTO.Country;
using firstAPI.Models;
using firstAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace firstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryRepository countryRepository, IMapper mapper, ILogger<CountryController> logger)
        {
            _countryRepository= countryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetAll()
        {
            var countries = await _countryRepository.GetAll();
            var countriesDto = _mapper.Map<List<CountryDto>>(countries);
            if(countries == null)
            {
                return NoContent();
            }
            return Ok(countriesDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CountryDto>> GetByID(int id)
        {
            var country = await _countryRepository.GetById(id);
            var countryDto = _mapper.Map<CountryDto>(country);
            if (country == null)
            {
                _logger.LogError($"Error while try to get record id:{id}");
                return NoContent();
            }
            return Ok(countryDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateCountryDto>> Create([FromBody] CreateCountryDto countryDto)
        {
            var result = _countryRepository.IsCountryExsits(countryDto.Name);

            if (result)
            {
                return Conflict("Country already exsits in database");
            }

            //Country country = new Country();
            //country.Name = countryDto.Name;
            //country.ShortName= countryDto.ShortName;
            //country.CountryCode= countryDto.CountryCode;

            var country = _mapper.Map<Country>(countryDto);

            await _countryRepository.Create(country);
            return CreatedAtAction("GetById", new { id = country.Id }, country);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Country>> Update(int id, [FromBody] UpdateCountryDto countryDto)
        {
            if (countryDto == null || id != countryDto.Id)
            {
                return BadRequest();
            }

            var country = _mapper.Map<Country>(countryDto);

            await _countryRepository.Update(country);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var country = await _countryRepository.GetById(id);
            await _countryRepository.Delete(country);
            return NoContent();
        }
    }
}
