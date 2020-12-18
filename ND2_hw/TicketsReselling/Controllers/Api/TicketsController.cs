using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketsReselling.Business.Models;
using TicketsReselling.Controllers.Api.Models;
using TicketsReselling.Core.Controllers.Api.Models;
using TicketsReselling.Core.Interfaces;
using TicketsReselling.DAL.Enums;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Controllers.Api
{
    [Authorize(Roles = UserRoles.Broker)]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TicketsControllerAPI : Controller
    {

        private readonly ITicketsService ticketsService;
        private readonly IOrdersService ordersService;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public TicketsControllerAPI(
            ITicketsService ticketsService,
            IOrdersService ordersService,
            IMapper mapper,
            UserManager<User> userManager)
        {
            this.ticketsService = ticketsService;
            this.ordersService = ordersService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        // GET
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<TicketResource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserTicketsByEvent(int eventId)
        {
            var currentUserId = userManager.GetUserId(User);
            var userTickets = await ticketsService.GetUserTicketsByEventId(eventId, currentUserId);

            var result = mapper.Map<IEnumerable<TicketResource>>(userTickets);

            return Ok(result);
        }

        // POST
        [HttpPost]
        [Route("addListing")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddListingOfTickets([FromBody] ListingResource listing )
        {
            var ticket = new Ticket
            {
                EventId = listing.EventId,
                SellerId = userManager.GetUserId(User),
                Price = listing.Price,
                SellerNotes = listing.Notes,
                Status = TicketStatuses.Selling
            };

            await ticketsService.AddListingWithTickets(ticket, listing.Amount, listing.ListingName);

            return Ok();
        }

        // POST
        [HttpPost]
        [Route("removeTicket")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveTicket([FromBody] TicketResource ticketResourse)
        {
            var ticket = await ticketsService.GetTicketById(ticketResourse.Id);
            await ticketsService.ChangeTicketStatus(ticket, TicketStatuses.Removed);

            return Ok();
        }

        // POST
        [HttpPost]
        [Route("confirmOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfirmOrder([FromBody] TicketResource ticketResourse)
        {
            var ticket = await ticketsService.GetTicketById(ticketResourse.Id);
            var order = await ordersService.GetOrderByTicketIdAndStatuses(ticket.Id, OrderStatuses.WaitingForConfirmation);

            await ticketsService.ChangeTicketStatus(ticket, TicketStatuses.WaitingForReceivingConfirmation);
            await ordersService.ChangeOrderStatus(order, OrderStatuses.Confirmed);

            return Ok();
        }

        // POST
        [HttpPost]
        [Route("rejectOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RejectOrder([FromBody] TicketResource ticketResourse)
        {
            var ticket = await ticketsService.GetTicketById(ticketResourse.Id);
            var order = await ordersService.GetOrderByTicketIdAndStatuses(ticket.Id, OrderStatuses.WaitingForConfirmation);

            await ticketsService.ChangeTicketStatus(ticket, TicketStatuses.Selling);
            await ordersService.ChangeOrderStatus(order, OrderStatuses.Rejected);

            return Ok();
        }

        // POST
        [HttpPost]
        [Route("addTracking")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddTracking([FromBody] TrackingResource trackingResourse)
        {
            var ticket = await ticketsService.GetTicketById(trackingResourse.ticketId);
            var order = await ordersService.GetOrderByTicketIdAndStatuses(ticket.Id, OrderStatuses.Confirmed);

            await ordersService.ChangeOrderTracking(order, trackingResourse.TrackingNumber);

            return Ok();
        }
    }
}