import { loadCategories, addListing, getTicketsForListings, deleteTicket, loadEvents } from "../API/store";

const ADD_CATEGORY = "ADD_CATEGORY";
const REMOVE_CATEGORY = "REMOVE_CATEGORY";
const CLEAR_CATEGORIES = "CLEAR_CATEGORIES";
const GET_EVENTS = "GET_EVENTS";
const TOGGLE_IS_LOADING = "TOGGLE_IS_LOADING";
const TOGGLE_IS_CATEGORIES_LOADING = "TOGGLE_IS_CATEGORIES_LOADING";
const LOAD_CATEGORIES = "LOAD_CATEGORIES";
const IS_WITH_MY_TICKETS = "IS_WITH_MY_TICKETS";
const OPEN_ADD_LISTING_MODAL = "OPEN_ADD_LISTING_MODAL";
const CLOSE_ADD_LISTING_MODAL = "CLOSE_ADD_LISTING_MODAL";
const LOAD_LISTINGS = "LOAD_LISTINGS";

const initialState = {
  categories: [],
  events: [],
  listings: [],
  filters: {
    categories: [],
    withUserTicketsOnly: false
  },
  addListing: {
    isAddListingModalOpened: false,
    eventId: ""
  },
  isLoading: false,
  isCategoriesLoading: false
};

const catalogReducer = (state = initialState, action) => {
  switch (action.type) {
    case ADD_CATEGORY:
      return {
        ...state,
        filters: {
          ...state.filters,
          categories: [...state.filters.categories, action.id],
        },
      };
    case REMOVE_CATEGORY:
      return {
        ...state,
        filters: {
          ...state.filters,
          categories: state.filters.categories.filter((i) => i !== action.id),
        },
      };
    case CLEAR_CATEGORIES:
      return {
        ...state,
        filters: {
          ...state.filters,
          categories: [],
        },
      };
    case GET_EVENTS:
      return {
        ...state,
        events: action.events,
      };
    case TOGGLE_IS_LOADING: {
      return { ...state, isLoading: !state.isLoading };
    }
    case TOGGLE_IS_CATEGORIES_LOADING: {
      return { ...state, isCategoriesLoading: !state.isCategoriesLoading };
    }
    case LOAD_CATEGORIES: {
      return { ...state, categories: action.categories };
    }
    case IS_WITH_MY_TICKETS: {
      return {
        ...state,
        filters: {
          ...state.filters,
          withUserTicketsOnly: action.value,
        }
      }
    }
    case OPEN_ADD_LISTING_MODAL: {
      return {
        ...state,
        addListing: {
          isAddListingModalOpened: true,
          eventId: action.eventId
        }
      };
    }
    case CLOSE_ADD_LISTING_MODAL: {
      return {
        ...state,
        addListing: {
          isAddListingModalOpened: false,
          eventId: ""
        }
      };
    }
    case LOAD_LISTINGS: {
      return {
        ...state,
        listings: action.listings
      };
    }
    default:
      return state;
  }
};

export const changeCategoryActionCreator = (id, value) =>
  value ? { type: ADD_CATEGORY, id } : { type: REMOVE_CATEGORY, id };

export const clearCategoriesActionCreator = () => ({ type: CLEAR_CATEGORIES });

export const getEventsActionCreator = (events) => ({
  type: GET_EVENTS,
  events,
});

export const loadCategoriesActionCreator = (categories) => ({
  type: LOAD_CATEGORIES,
  categories,
});

export const toggleIsLoading = () => ({
  type: TOGGLE_IS_LOADING,
});

export const toggleIsCategoriesLoading = () => ({
  type: TOGGLE_IS_CATEGORIES_LOADING,
});

export const toggleIsWithMyTickets = (value) => ({
  type: IS_WITH_MY_TICKETS, value
});

export const openAddListingModal = (eventId) => ({
  type: OPEN_ADD_LISTING_MODAL,
  eventId
});

export const closeAddListingModal = () => ({
  type: CLOSE_ADD_LISTING_MODAL
})

export const loadListingsActionCreator = (listings) => ({
  type: LOAD_LISTINGS,
  listings
})

export default catalogReducer;

export const getEvents = (filters) => {
  return (dispatch) => {
    dispatch(toggleIsLoading());
    loadEvents(filters).then((data) => {
      dispatch(toggleIsLoading());
      dispatch(getEventsActionCreator(data));
    });
  };
};

export const getCategories = () => {
  return (dispatch) => {
    dispatch(toggleIsCategoriesLoading());
    loadCategories().then((data) => {
      dispatch(toggleIsCategoriesLoading());
      dispatch(loadCategoriesActionCreator(data));
    });
  };
};

export const sendListing = (listing) => {
  return (dispatch) => {
    dispatch(toggleIsLoading());
    addListing(listing).then(() => {
      dispatch(closeAddListingModal());
      dispatch(getEvents());
      dispatch(toggleIsLoading());
    });
  }
}

export const getListings = eventId => {
  return (dispatch) => {
    dispatch(toggleIsLoading());
    getTicketsForListings(eventId).then((data) => {
      dispatch(loadListingsActionCreator(data));
      dispatch(toggleIsLoading());
    });
  }
}

export const invokeTicketAction = (actionType, ticketId, eventId) => {
  var action = () => { };
  switch (actionType) {
    case "delete":
      action = deleteTicket;
      break;
    default:
      break;
  }

  return (dispatch) => {
    dispatch(toggleIsLoading());
    action(ticketId).then(() => {
      dispatch(getListings(eventId));
      dispatch(toggleIsLoading());
    });
  }
}