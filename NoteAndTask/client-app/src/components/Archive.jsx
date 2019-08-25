import React from "react";
import { request } from "../libs/api";
import { DashBoard } from "./layout/DashBoard";
import "./Tasks.css";
import "../styles/default.css";

export class Archive extends React.Component {
  static displayName = Archive.name;

  constructor(props) {
    super(props);
    this.state = { tasks: [], loading: false };

    this.loadTasks();
  }

  loadTasks() {
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

  render() {
    return (
      <DashBoard>
        {this.state.tasks && (
          <div>
            {this.state.tasks.map(task => (
              <div
                className="task-card shadow-box"
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
