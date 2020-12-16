import React from "react";
import { NavLink } from "react-router-dom";
import { EVENTS_WITH_MY_LISTINGS, EVENTS_PATH } from "../../constants";
import style from "./Navigation.module.css";

const Navigation = () => {
  return (
    <nav className={style.navigation}>
      <NavLink to={EVENTS_PATH} activeClassName={style.active}>All events</NavLink>
      <NavLink to={EVENTS_WITH_MY_LISTINGS} activeClassName={style.active}>Events with my listings</NavLink>
    </nav>
  );
};

export default Navigation;
