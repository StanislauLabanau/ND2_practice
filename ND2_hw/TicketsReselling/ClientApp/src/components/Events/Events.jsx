import React, { Component } from "react";
import { connect } from "react-redux";
import { getEvents, toggleIsWithMyTickets, openAddListingModal } from "../../redux/catalog-reducer";
import EventItems from "../EventItems/EventItems";
import Categories from "../Categories/Categories";
import Filter from "../Filter/Filter";
import Preloader from "../Preloader/Preloader";
import style from "./Events.module.css";

class Events extends Component {
  componentDidMount() {
    this.props.getEvents(this.props.filters);
    this.props.toggleIsWithMyTickets(this.props.myListings === "yes");
  }

  componentDidUpdate(prevProps, prevState, snapshot) {

    if (prevProps.myListings !== this.props.myListings) {
      this.props.toggleIsWithMyTickets(this.props.myListings === "yes")
    }

    if (prevProps.filters.categories !== this.props.filters.categories ||
      prevProps.filters.withUserTicketsOnly !== this.props.filters.withUserTicketsOnly) {
      this.props.getEvents(this.props.filters);
    }
  }

  onAddListingHandler = (eventId) => {
    this.props.openAddListingModal(eventId);
  }

  render() {
    if (this.props.events.length === 0) {
      return <h2>No event found</h2>;
  }
    return (
      <div className={style.catalog}>
        <div className={style.categories}>
          <Categories
            categories={this.props.categories}
            selectedCategories={this.props.filters.categories}
            onCategoryChange={this.props.onCategoryChange}
            clearSelectedCategories={this.props.clearSelectedCategories}
          />
        </div>
        <div className={style.filters}>
          <Filter />
        </div>
        <div className={style.items}>
          {this.props.isLoading ? (
            <Preloader />
          ) : (
              <EventItems events={this.props.events} onAddListingClick={this.onAddListingHandler} />
            )}
        </div>
      </div>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    events: state.catalog.events,
    filters: state.catalog.filters,
    isLoading: state.catalog.isLoading,
  };
};

export default connect(mapStateToProps, {
  getEvents,
  toggleIsWithMyTickets,
  openAddListingModal
})(Events);
