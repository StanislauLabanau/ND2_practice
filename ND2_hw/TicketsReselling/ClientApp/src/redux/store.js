import { combineReducers, createStore, applyMiddleware, compose } from "redux";
import { reducer as formReducer } from "redux-form";
import catalogReducer from "./catalog-reducer";
import thunk from "redux-thunk";

const reducers = combineReducers({
  catalog: catalogReducer,
  form: formReducer
});

const enhancers = compose(
  applyMiddleware(thunk),
  window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
);

const store = createStore(reducers, enhancers);

export default store;
