import React from "react";
import {
  BrowserRouter as Router,
  Redirect,
  Route,
  Switch,
} from "react-router-dom";
import "./App.css";
import AddListingModal from "./components/AddListingModal/AddListingModal";
import Events from "./components/Events/Events";
import ListingsManager from "./components/ListingsManager/ListingsManager";
import Header from "./components/Header/Header";
import { EVENTS_WITH_MY_LISTINGS, EVENTS_PATH, MANAGE_MY_LISTINGS } from "./constants";

const App = (props) => {
  return (
    <Router basename="ClientApp"> 
      <div className="App">
        <Header />
        <Switch>
          <Route path="/" exact render={() => <Redirect to={EVENTS_PATH} />} />
          <Route path={EVENTS_PATH}>
            <Events myListings="no"/>
          </Route>
          <Route path={EVENTS_WITH_MY_LISTINGS}>
            <Events myListings="yes"/>
          </Route>
          <Route path={MANAGE_MY_LISTINGS} component={ListingsManager}>
          </Route>
        </Switch>
      </div>
      <div className={"Modal"}>
        <AddListingModal />
      </div>
    </Router>
  );
};

export default App;
