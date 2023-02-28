using System.ComponentModel.DataAnnotations;

namespace firstAPI.DTO.Country
{
    public class UpdateCountryDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string CountryCode { get; set; }
    }
}
