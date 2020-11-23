using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketsReselling.Core.Controllers.Api.Models;
using TicketsReselling.Core.Interfaces;
using TicketsReselling.Core.Queries;
using TicketsReselling.DAL.Enums;


namespace TicketsReselling.Controllers.Api
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VenuesController : Controller
    {
        private readonly IVenuesService venuesService;
        private readonly IMapper mapper;

        public VenuesController(
            IVenuesService venuesService,
            IMapper mapper)
        {
            this.mapper = mapper;
            this.venuesService = venuesService;
        }

        // GET
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EventResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVenuesByQuery([FromQuery] VenueQuery query)
        {
            var pagedResult = await venuesService.GetVenuesByStatusesAndQuery(query, VenueStatuses.Avaliable);

            var result = mapper.Map<IEnumerable<VenueResource>>(pagedResult);

            return Ok(result);
        }
    }
}