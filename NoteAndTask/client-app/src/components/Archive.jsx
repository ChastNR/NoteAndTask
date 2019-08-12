import React from "react";
import { request } from "../libs/api";
import { DashBoard } from "./layout/DashBoard";
import "./Tasks.css";

export class Archive extends React.Component {
  static displayName = Archive.name;

  constructor(props) {
    super(props);
    this.state = { tasks: [], loading: false };

    request("/api/task/get?archived=true").then(data => {
      this.setState({ tasks: data, loading: true });
    });
  }

  taskOpen = id => {
    const task = document.getElementById(id);
    if (task.style.display === "none" || task.style.display.length === 0) {
      task.style.display = "block";
    } else {
      task.style.display = "none";
    }
  };

  renderTasks(tasks) {
    return (
      <div className="row">
        {tasks.map(task => (
          <div className="card m-3 shadow-sm" key={task.id}>
            <div className="card-header">
              <h6>
                <i className="fas fa-thumbtack mr-2" />
                {task.name}
              </h6>
            </div>
            <div className="card-body">
              <p className="card-text">{task.description}</p>
            </div>
            <div className="card-footer">
              <div className="small">Created: {task.creationDate}</div>
              <div className="small">
                <span>Expires:</span> {task.expiresOn}
              </div>
            </div>
          </div>
        ))}
      </div>
    );
  }

  render() {
    return (
      <DashBoard>
        {this.state.tasks && (
          <div>
            {this.state.tasks.map(task => (
              <div
                className="task-card shadow-sm"
                key={task.id}
                onClick={() => this.taskOpen(task.id)}
              >
                <div className="row align-items-center task-card-name">
                  <div>
                    {task.name} (Expires on: {task.expiresOn})
                  </div>
                </div>
                <div className="task-card-body" id={task.id}>
                  <hr />
                  <div>{task.description}</div>
                </div>
              </div>
            ))}
          </div>
        )}
      </DashBoard>
    );
  }
}
