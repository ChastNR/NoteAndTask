import React from "react";
import { request } from "../libs/api";
import { DashBoard } from "./layout/DashBoard";
import "./Tasks.css";
import "./css/global.css";
import { AddNewTaskModal } from "./layout/AddNewTaskModal";
import { req } from "../libs/gql";

export class Tasks extends React.Component {
  static displayName = Tasks.name;

  constructor(props) {
    super(props);
    this.state = { tasks: null, done: null, visible: false };

    this.loadTasks(this.props.match.params.id);
  }

  // loadTasks(id) {
  //   if (id) {
  //     request("/api/task/get?id=" + id).then(data => {
  //       this.setState({ tasks: data });
  //     });
  //   } else {
  //     request("/api/task/get").then(data => {
  //       this.setState({ tasks: data });
  //     });
  //   }
  // }

  loadTasks(id) {
    if (id) {
      req({
        query: "{tasks(id: " + id + ") {id, name, expiresOn, description}}"
      }).then(response => {
        this.setState({ tasks: response.data.tasks });
      });
    } else {
      req({ query: "{tasks {id, name, expiresOn, description}}" }).then(
        response => {
          this.setState({ tasks: response.data.tasks });
        }
      );
    }
  }

  componentWillReceiveProps(newProps) {
    this.loadTasks(newProps.match.params.id);
  }

  taskOpen = id => {
    const task = document.getElementById(id);
    if (task.style.display === "none" || task.style.display.length === 0) {
      task.style.display = "block";
    } else {
      task.style.display = "none";
    }
  };

  taskDone = async id => {
    await request("/api/task/done?Id=" + id);
    this.loadTasks();
  };

  render() {
    return (
      <DashBoard>
        <div className="task-card shadow-sm">
          <div className="row align-items-center task-card-name">
            <AddNewTaskModal />
          </div>
        </div>
        {this.state.tasks && (
          <div>
            {this.state.tasks.map(task => (
              <div className="task-card shadow-sm" key={task.id}>
                <div
                  className="row align-items-center task-card-name"
                  onClick={() => this.taskOpen(task.id)}
                >
                  <div>
                    {task.name} (Expires on: {task.expiresOn})
                  </div>
                </div>
                <div className="task-card-body" id={task.id}>
                  <hr />
                  <div>{task.description}</div>
                  <hr />
                  <div className="card-done-button">
                    <a
                      href="#"
                      className="card-done-button"
                      onClick={() => this.taskDone(task.id)}
                    >
                      Done
                    </a>
                  </div>
                </div>
              </div>
            ))}
          </div>
        )}
      </DashBoard>
    );
  }
}
