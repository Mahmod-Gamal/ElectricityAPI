using ElectricityAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectricityAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ElectricityController : ControllerBase
    {

        protected readonly ElectricityContext _context;

        public ElectricityController(ElectricityContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult>> Get([FromQuery] int page = 1,
                                                                [FromQuery] int pageSize = 10,
                                                                [FromQuery] int? govId = null,
                                                                [FromQuery] int? cityId = null,
                                                                [FromQuery] int? areaId = null,
                                                                [FromQuery] string searchText = null
                                                             )
        {
            List<Result> result = new List<Result>();
            var areas = _context.Areas.Include(a => a.Time).Include(a => a.City).ThenInclude(c => c.Gov).AsQueryable();
            if (areaId != null)
                areas = areas.Where(a => a.Id == (int)areaId);
            else if (cityId != null)
                areas = areas.Where(a => a.CityId == (int)cityId);
            else if (govId != null)
                areas = areas.Where(a => a.City.GovId == (int)govId);

            var areasData = areas
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToList();


            foreach (var area in areasData)
            {
                var r = new Result()
                {
                    AreaID = area.Id,
                    AreaName = area.Area1,
                    CityID = area.CityId,
                    CityName = area.City.City1,
                    GovID = area.City.GovId,
                    GovName = area.City.Gov.Gov,
                    HourFrom = area.Time.HourFrom,
                    MinuteFrom = area.Time.MinutesFrom,
                    HourTo = area.Time.HourTo,
                    MinuteTo = area.Time.MinutesTo,

                };
                result.Add(r);
            }
            return new PagedResult()
            {
                Result = result,
                TotalRecords = result.Count(),
                PageNumber = page,
                PageSize = pageSize
            };
        }

        [HttpGet("Govs")]
        public async Task<ActionResult<IEnumerable<GovDto>>> GetGovs()
        {
            List<GovDto> govsDto = new List<GovDto>();

            var governorates = await _context.Governorates.ToListAsync();
            foreach (var governorate in governorates)
            {
                govsDto.Add(new GovDto() { Id = governorate.Id, Gov = governorate.Gov });
            }
            return govsDto;
        }


        [HttpGet("Gov/{govId}/Cities")]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities(int govId)
        {
            List<CityDto> citiesDto = new List<CityDto>();

            var cities = await _context.Cities.Where(c => c.GovId == govId).ToListAsync();
            foreach (var city in cities)
            {
                citiesDto.Add(new CityDto() { Id = city.Id, City = city.City1 });
            }
            return citiesDto;
        }


        [HttpGet("City/{cityId}/Areas")]
        public async Task<ActionResult<IEnumerable<AreaDto>>> GetAreas(int cityId)
        {
            List<AreaDto> areaDto = new List<AreaDto>();

            var areas = await _context.Areas.Where(c => c.CityId == cityId).ToListAsync();
            foreach (var area in areas)
            {
                areaDto.Add(new AreaDto() { Id = area.Id, Area = area.Area1 });
            }
            return areaDto;
        }

    }
}


