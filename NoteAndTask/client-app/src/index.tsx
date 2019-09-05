import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import App from "./App";
import * as serviceWorker from "./serviceWorker";
import { Provider } from "mobx-react";
import { ApplicationStore } from "./components/ApplicationStore";
import { ListsStore } from "./stores/ListsStore";
import { TasksStore } from "./stores/TasksStore";
import { UserStore } from "./stores/UserStore";

ReactDOM.render(
  <Provider applicationStore={new ApplicationStore()} listsStore={new ListsStore()} tasksStore={new TasksStore()} userStore={new UserStore()}>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </Provider >,
  document.getElementById("root")
);

serviceWorker.register();
