using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketsReselling.Core.Controllers.Api.Models;
using TicketsReselling.Core.Interfaces;
using TicketsReselling.Core.Queries;
using TicketsReselling.DAL.Enums;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Controllers.Api
{
    [ApiController]
    [FormatFilter]
    [Route("api/v1/[controller]")]
    public class EventsController : Controller
    {
        private readonly IEventsService eventsService;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public EventsController(
            IEventsService eventsService,
            IMapper mapper,
             UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.eventsService = eventsService;
            this.userManager = userManager;
        }

        // GET
        [HttpGet]
        [Route("")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(typeof(IEnumerable<EventResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEventsByQuery([FromQuery] EventQuery query)
        {
            query.CurrentUserId = userManager.GetUserId(User);
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

        // GET
        [HttpGet]
        [Route("categories")]
        [ProducesResponseType(typeof(IEnumerable<CategoryResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
        {
            var pagedResult = await eventsService.GetCategories();

            var result = mapper.Map<IEnumerable<CategoryResource>>(pagedResult);

            return Ok(result);
        }
    }
}