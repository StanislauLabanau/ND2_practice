using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketsReselling.Business;
using TicketsReselling.Business.Models;
using TicketsReselling.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketsReselling.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventsRepository eventsRepository;

        public EventsController(EventsRepository eventsRepository)
        {
            this.eventsRepository = eventsRepository;
        }

        public IActionResult Index()
        {
            var model = new EventsViewModel
            {
                Categories = eventsRepository.GetCategories(),
                Events = eventsRepository.GetEvents()
            };

            return View(model);
        }
    }
}
