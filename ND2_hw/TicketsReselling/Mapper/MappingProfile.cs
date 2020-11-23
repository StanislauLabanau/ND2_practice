using AutoMapper;
using TicketsReselling.Core.Controllers.Api.Models;
using TicketsReselling.DAL.Models;

namespace TicketsReselling.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventResource>()
                .ForMember(m => m.VenueName, opt => opt.MapFrom(src => src.Venue.Name))
                .ForMember(m => m.CityName, opt => opt.MapFrom(src => src.Venue.City.Name));
            CreateMap<Venue, VenueResource>();
        }
    }
}