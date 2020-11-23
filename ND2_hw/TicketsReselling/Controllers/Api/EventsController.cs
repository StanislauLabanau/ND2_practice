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
    [FormatFilter]
    [Route("api/v1/[controller]")]
    public class EventsController : Controller
    {
        private readonly IEventsService eventsService;
        private readonly IMapper mapper;

        public EventsController(
            IEventsService eventsService,
            IMapper mapper)
        {
            this.mapper = mapper;
            this.eventsService = eventsService;
        }

        // GET
        [HttpGet]
        [Route("")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(typeof(IEnumerable<EventResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEventsByQuery([FromQuery] EventQuery query)
        {
            var pagedResult = await eventsService.GetEventsByStatusAndQuery(EventStatuses.Current, query);
            HttpContext.Response.Headers.Add("x-total-count", pagedResult.TotalCount.ToString());

            return Ok(mapper.Map<IEnumerable<EventResource>>(pagedResult.Items));
        }

        [HttpGet]
        [Route("suggestions")]
        [ProducesResponseType(typeof(IEnumerable<EventResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSuggestedEvents([FromQuery] EventQuery query)
        {
            var suggestedEvents = await eventsService.GetSuggestedEvents(EventStatuses.Current, query, 10);

            return Ok(new JsonResult(suggestedEvents));
        }
    }
}