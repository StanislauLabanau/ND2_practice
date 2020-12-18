import "bootstrap";
import "bootstrap-select";
import "bootstrap-autocomplete";

const filtersEvent = {
    categories: [],
    cities: [],
    venues: [],
    searchText: "",
    page: 1,
    pageSize: 4,
    sortBy: 0,
    sortOrder: 0,
    fromDate: "",
    toDate: "",
};

const filtersVenue = {
    cities: [],
};


// Also it's possible to go to controller action with this <a href="events/EventWithTickets?eventid=${event.id}" class="btn btn-primary pt-1 mb-1">Show tickets</a>

function createEvent(event) {
    return `<div class="col mb-4">
                        <div class="card text-center">
                            <img src="/public/${event.banner}" alt="${event.name}" class="card-img-top img-fluid" />
                            <h4>${event.name}</h4>
                            <h5>${event.cityName}</h5>
                            <h5>${event.venueName}</h5>
                            <h5>${event.date}</h5>
                            <a href="${props.showEventUrl + "?eventId=" + event.id}" class="btn btn-primary pt-1 mb-1">Show tickets</a>
                            ${props.userInAdminRole === "true" ? "<a href=\"" + props.removeEventUrl + "?eventId=" + event.id + "\" class=\"btn btn-danger pt-1 mb-1\">Remove event</a>" : ""}
                        </div>
                    </div>`};

function createVenue(venue) {
    return `<option value="${venue.id}">${venue.name}</option>`
};

$(document).ready(function () {
    getEvents();
    getVenues();

    //$("#autosuggest").autocomplete({
    //    source: "api/v1/events/suggestions",
    //    minLength: 2,
    //    position: {
    //        my: "left top",
    //        at: "left-23 bottom+10"
    //    }
    //});

    $("#autosuggest").autoComplete({
        resolver: "custom",
        events: {
            search: function (searchText, response) {
                $.ajax({
                    url: "api/v1/events/suggestions",
                    data: { ...filtersEvent, searchText: searchText },
                    traditional: true,
                    success: function (data, status, xhr) {
                        const showData = data.value.map(item => {
                            return { value: item.name, text: item.name };
                        });
                        response(showData);
                    }
                });
            }
        }
    });

    $("#search").on("click", function () {
        filtersEvent.searchText = $("#autosuggest").val();
        getEvents();
    });

    $("#category").on("change", function () {
        filtersEvent.categories = $(this).val();
        getEvents();
    });

    $("#city").on("change", function () {
        filtersVenue.cities = $(this).val();
        filtersEvent.cities = $(this).val();
        filtersEvent.venues = [];
        getVenues();
        getEvents();
    });

    $("#venue").on("change", function () {
        filtersEvent.venues = $(this).val();
        getEvents();
    });

    $("#fromDate").on("change", function () {
        filtersEvent.fromDate = $(this).val();
        getEvents();
    });

    $("#toDate").on("change", function () {
        filtersEvent.toDate = $(this).val();
        getEvents();
    });

    $("#sortBy").on("change", function () {
        filtersEvent.sortBy = $(this).val();
        getEvents();
    });

    $("#sortOrder").on("change", function () {
        filtersEvent.sortOrder = $(this).val();
        getEvents();
    });

    $("#pageSize").on("change", function () {
        filtersEvent.pageSize = $(this).val();
        getEvents();
    });
});

function addZeroDate(value) {
    if (value < 10) {
        value = '0' + value;
    }
    return value;
}

function getEvents() {
    $.ajax({
        url: "api/v1/events",
        data: filtersEvent,
        traditional: true,
        success: function (data, status, xhr) {
            data = data.map(item => {
                const date = new Date(item.date);
                item.date = `${addZeroDate(date.getDate())}.${addZeroDate(date.getMonth() + 1)}.${date.getFullYear()}`;
                return item;
            });
            $("#events").empty().append($.map(data, createEvent));
            const count = xhr.getResponseHeader("x-total-count");
            addPaginationButtons(filtersEvent.page, count, filtersEvent.pageSize);
        }
    });
}

function getVenues() {
    $.ajax({
        url: "/api/v1/venues",
        data: filtersVenue,
        traditional: true,
        success: function (data, status) {
            $("#venue").empty().append($.map(data, createVenue));
            $("#venue").selectpicker("refresh");
        }
    });
}

function addPaginationButtons(currentPage, totalCount, pageSize) {
    const pageCount = Math.ceil(totalCount / pageSize);
    const buttons = [];
    for (let i = 1; i <= pageCount; i++) {
        const button = $("<li>", { class: "page-item" });
        if (i === currentPage) {
            button.addClass("active");
            button.append(`<a class="page-link" href="#">${i} <span class="sr-only">(current)</span></a>`)
        } else {
            button.append(`<a class="page-link" href="#">${i}</a>`)
        }
        button.data("page", i);
        buttons.push(button);
    }
    $(".pagination").empty().append(buttons);
    $(".page-item").on("click", function () {
        filtersEvent.page = $(this).data("page");
        getEvents();
    });
}
