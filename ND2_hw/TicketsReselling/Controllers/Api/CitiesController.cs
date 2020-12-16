using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketsReselling.Core.Controllers.Api.Models;
using TicketsReselling.Core.Interfaces;
using TicketsReselling.DAL.Enums;


namespace TicketsReselling.Controllers.Api
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CitiesController : Controller
    {
        private readonly  ICitiesService citiesService;
        private readonly IMapper mapper;

        public CitiesController(
            ICitiesService citiesService,
            IMapper mapper)
        {
            this.mapper = mapper;
            this.citiesService = citiesService;
        }

        // GET
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CityResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVenuesByQuery()
        {
            var pagedResult = await citiesService.GetCityesByStatus(CityStatuses.Avaliable);

            var result = mapper.Map<IEnumerable<CityResource>>(pagedResult);

            return Ok(result);
        }
    }
}