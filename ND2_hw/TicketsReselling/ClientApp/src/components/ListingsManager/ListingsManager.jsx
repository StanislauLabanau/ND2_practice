import React, { Component } from "react";
import { connect } from "react-redux";
import { getListings, invokeTicketAction } from "../../redux/catalog-reducer";
import Preloader from "../Preloader/Preloader";
import style from "./ListingsManager.module.css";

const STATUSES = [
    "WaitingForConfirmation",
    "WaitingForReceivingConfirmation",
    "Selling",
    "Sold"
];

const ACTIONS = {
    Selling: [
        {
            name: "Delete",
            type: "delete"
        }
    ]
};

class ListingsManager extends Component {
    componentDidMount() {
        this.props.getListings(this.props.match.params.eventId);
    }

    addZeroDate = value => {
        if (value < 10) {
            value = '0' + value;
        }
        return value;
    }

    onAction = (action, ticketId) => e => {
        e.preventDefault();
        this.props.invokeTicketAction(action, ticketId, this.props.match.params.eventId);
    }

    render() {

        if (this.props.isLoading) {
            return <Preloader />;
        }

        if (this.props.listings.length === 0) {
            return <h2>No listings for this event</h2>;
        }

        const ticketsGroupedByStatus = this.props.listings
            .reduce((hash, { status: value, ...rest }) => ({ ...hash, [value]: (hash[value] || []).concat({ status: value, ...rest }) }), {});

        const event = this.props.listings[0].listing.event;
        const date = new Date(event.date);
        const dateStr = `${this.addZeroDate(date.getDate())}.${this.addZeroDate(date.getMonth() + 1)}.${date.getFullYear()}`;
        return <div className={style.listingsWrapper}>
            <h2 style={style.h2}>{event.name} {dateStr}</h2>
            <div className={style.listingsHeader}>
                <div>Listing</div>
                <div>Price (USD)</div>
                <div>Actions</div>
            </div>
            {STATUSES.map(status => {
                return <React.Fragment key={status}>
                    <div className={style.statusName}>{status}</div>
                    {ticketsGroupedByStatus[status] ?
                        ticketsGroupedByStatus[status].map(ticket => {
                            return <div key={ticket.id} className={style.ticketItem}>
                                <div>{ticket.listing.name}</div>
                                <div>{ticket.price}</div>
                                <div>{ACTIONS[status].map(action => {
                                    return <button key={action.type} type="button" onClick={this.onAction(action.type, ticket.id)}>{action.name}</button>
                                })}</div>
                            </div>
                        })
                        :
                        <div>No tickets</div>
                    }
                </React.Fragment>
            })
            }
        </div >;
    }
}

const mapStateToProps = (state) => {
    return {
        listings: state.catalog.listings,
        isLoading: state.catalog.isLoading
    };
};

export default connect(mapStateToProps, {
    getListings,
    invokeTicketAction
})(ListingsManager);
