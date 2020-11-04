using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TicketsReselling.Business.Models;
using TicketsReselling.Core;
using TicketsReselling.DAL.Enums;
using TicketsReselling.DAL.Models;
using TicketsReselling.Models;

namespace TicketsReselling.Controllers
{
    [Authorize(Roles = UserRoles.Administrator)]
    public class VenuesController : Controller
    {
        private readonly VenuesService venuesService;
        private readonly CitiesService citiesService;
        private readonly IStringLocalizer<EventsController> stringLocalizer;

        public VenuesController(
            VenuesService venuesService,
            CitiesService citiesService,
            IStringLocalizer<EventsController> stringLocalizer)
        {
            this.venuesService = venuesService;
            this.citiesService = citiesService;
            this.stringLocalizer = stringLocalizer;
        }

        public async Task<IActionResult> Index(int cityId = 1)
        {
            ViewBag.cityId = cityId;
            ViewBag.cityName = (await citiesService.GetCityById(cityId))?.Name;
            ViewBag.cityStatus = (await citiesService.GetCityById(cityId))?.Status;

            var model = new VenuesViewModel
            {
                Cities = await citiesService.GetCityesByStatus(CityStatuses.Avaliable, CityStatuses.NotAvaliable),
                Venues = await venuesService.GetVenuesByStatus(VenueStatuses.Avaliable, VenueStatuses.NotAvaliable)
            };

            return View(model);
        }

        public IActionResult AddCity()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCity(City newCity)
        {
            if (ModelState.IsValid)
            {
                await citiesService.AddCity(newCity);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveCity(int cityId)
        {
            var city = await citiesService.GetCityById(cityId);
            await citiesService.ChangeCityStatus(city, CityStatuses.Removed);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddVenue(int cityId)
        {
            ViewBag.cityId = cityId;
            ViewBag.cityName = (await citiesService.GetCityById(cityId))?.Name;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVenue(Venue newVenue, string returnUrl, string queryString)
        {
            if (ModelState.IsValid)
            {
                await venuesService.AddVenue(newVenue);
            }

            return RedirectToTheSamePage(returnUrl, queryString);
        }

        public async Task<IActionResult> RemoveVenue(int venueId, string returnUrl, string queryString)
        {
            var venue = await venuesService.GetVenueById(venueId);
            await venuesService.ChangeVenueStatus(venue, VenueStatuses.Removed);

            return RedirectToTheSamePage(returnUrl, queryString);
        }

        public async Task<IActionResult> MakeCityAvaliable(int cityId, string returnUrl, string queryString)
        {
            await citiesService.MakeCityAndAllItsVenuesAvaliable (cityId);

            return RedirectToTheSamePage(returnUrl, queryString);
        }

        public async Task<IActionResult> MakeCityNotAvaliable(int cityId, string returnUrl, string queryString)
        {
            await citiesService.MakeCityAndAllItsVenuesNotAvaliable(cityId);

            return RedirectToTheSamePage(returnUrl, queryString);
        }

        public async Task<IActionResult> MakeVanueAvaliable(int venueId, string returnUrl, string queryString)
        {
            var venue = await venuesService.GetVenueById(venueId);
            await venuesService.ChangeVenueStatus(venue, VenueStatuses.Avaliable);

            return RedirectToTheSamePage(returnUrl, queryString);
        }

        public async Task<IActionResult> MakeVanueNotAvaliable(int venueId, string returnUrl, string queryString)
        {
            var venue = await venuesService.GetVenueById(venueId);
            await venuesService.ChangeVenueStatus(venue, VenueStatuses.NotAvaliable);

            return RedirectToTheSamePage(returnUrl, queryString);
        }

        public IActionResult RedirectToTheSamePage(string returnUrl, string queryString)
        {
            if (!String.IsNullOrEmpty(returnUrl))
            {
                if (!String.IsNullOrEmpty(queryString))
                {
                    returnUrl += queryString;
                }

                return LocalRedirect(returnUrl);
            }

            return RedirectToAction("Index");
        }
    }
}
