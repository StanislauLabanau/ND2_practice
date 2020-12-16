import React from "react";
import styles from "./EventItems.module.css";
import { MANAGE_MY_LISTINGS } from "../../constants";
import { NavLink } from "react-router-dom";

const CatalogItems = (props) => {

  var addZeroDate = value => {
    if (value < 10) {
      value = '0' + value;
    }
    return value;
  }

  var addListing = eventId => e =>{
    e.preventDefault();
    props.onAddListingClick(eventId);
  }

  const items = props.products.map((p) => {
    const date = new Date(p.date);
    const dateStr = `${addZeroDate(date.getDate())}.${addZeroDate(date.getMonth() + 1)}.${date.getFullYear()}`;
    const manageListingsLink = MANAGE_MY_LISTINGS.replace(":eventId", p.id);
    return (
      <div className={styles.card} key={p.id}>
        <div className={styles.image}>
          <img alt="" src={`/Public/${p.banner}`} />
        </div>
        <div className="description">
          <span className={styles.name}>{p.name}</span>
          <span className={styles.name}>{p.cityName}</span>
          <span className={styles.name}>{p.venueName}</span>
          <span className={styles.name}>{dateStr}</span>
        </div>
        <button className="add" type="button" onClick={addListing(p.id)}>Add listing</button>
        <NavLink to={manageListingsLink}>Manage listings</NavLink>
      </div>
    );
  });

  return <div className={styles.items}>{items}</div>;
};

export default CatalogItems;
