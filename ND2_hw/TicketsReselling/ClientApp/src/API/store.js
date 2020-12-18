import axios from "axios";
import qs from "qs";

const instance = axios.create({
  baseURL: "/api/v1/",
  paramsSerializer: function (params) {
    return qs.stringify(params, { arrayFormat: "repeat" });
  },
});

export const loadEvents = (filters) => {
  return instance
    .get("Events", {
      params: filters,
    })
    .then((response) => {
      return response.data;
    });
};

export const loadCategories = () => {
  return instance.get("Events/Categories", {}).then((response) => {
    return response.data;
  });
};

export const addListing = async (listing) => {
  const response = await instance
    .post("TicketsAPI/addListing", {
      listingName: listing.listingName,
      eventId: listing.eventId,
      price: Number(listing.price),
      amount: Number(listing.amount),
      notes: listing.note
    });
  return response.data;
}

export const getTicketsForListings = eventId => {
  return instance
    .get("TicketsAPI", {
      params: {
        eventId: eventId
      }
    })
    .then((response)=>{
      return response.data;
    })
}

export const deleteTicket = async (ticketId) => {
  const response = await instance
    .post("TicketsAPI/removeTicket", {id: ticketId});
  return response.data;
}