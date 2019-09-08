import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import App from "./App";
import * as serviceWorker from "./serviceWorker";
import { Provider } from "mobx-react";
import { ListsStore } from "./stores/ListsStore";
import { TasksStore } from "./stores/TasksStore";
import { UserStore } from "./stores/UserStore";

ReactDOM.render(
  <Provider listsStore={new ListsStore()} tasksStore={new TasksStore()} userStore={new UserStore()}>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </Provider >,
  document.getElementById("root")
);

serviceWorker.register();
