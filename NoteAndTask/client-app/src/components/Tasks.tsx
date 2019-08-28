import React from "react";

import { request } from "../libs/api";
import { DashBoard } from "./layout/DashBoard";
import { AddNewTaskModal } from "./layout/AddNewTaskModal";
import { Button, Divider, Row } from "rsuite";

import "../styles/default.scss";
import "./Tasks.scss";

import { ITaskEntity } from "../interfaces/ITaskEntity";

interface ITasks {
  tasks: ITaskEntity[];
}

export class Tasks extends React.Component<any, ITasks> {
  constructor(props: any) {
    super(props);

    this.state = {
      tasks: []
    };

    this.loadTasks(this.props.match.params.id);
  }

  componentWillReceiveProps(newProps: any) {
    this.loadTasks(newProps.match.params.id);
  }

  loadTasks(id?: number) {
    if (id) {
      request("/api/task/get?id=" + id).then(data => {
        this.setState({ tasks: data } as ITasks);
      });
    } else {
      request("/api/task/get").then(data => {
        this.setState({ tasks: data } as ITasks);
      });
    }
  }

  taskOpen = (id: string) => {
    var task = document.getElementById(id);

    if (task != null) {
      if (task.style.length === 0) {
        task.style.display = "block";
      } else {
        task.style.display = "none";
      }
    }
  };

  taskDone = async (id: number) => {
    await request("/api/task/done?Id=" + id);
    this.loadTasks();
  };

  render() {
    return (
      <DashBoard>
        <div className="task-card shadow-box">
          <Row className="task-card-name">
            <AddNewTaskModal />
          </Row>
        </div>
        {this.state.tasks && (
          <div>
            {this.state.tasks.map(task => (
              <div className="task-card shadow-box" key={task.id}>
                <div
                  className="row align-items-center task-card-name"
                  onClick={() => this.taskOpen(task.id.toString())}
                >
                  <div>
                    {task.name} (Expires on: {task.expiresOn})
                  </div>
                </div>
                <div className="task-card-body" id={task.id.toString()}>
                  <Divider>Description</Divider>
                  <div>{task.description}</div>
                  <Divider />
                  <Button onClick={() => this.taskDone(task.id)} color="blue">
                    Done
                  </Button>
                </div>
              </div>
            ))}
          </div>
        )}
      </DashBoard>
    );
  }
}
