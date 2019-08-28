import * as React from "react";
import { request } from "../libs/api";
import { DashBoard } from "./layout/DashBoard";
import { AddNewTaskModal } from "./layout/AddNewTaskModal";
import { Button, Divider, Row } from "rsuite";

import "../styles/default.css";
import "./Tasks.scss";

interface ITaskEntity {
  Id: number;
  Name: string;
  Description: string;
  IsDone: boolean;
  ExpiresOn: Date;
  CreationDate: Date;
  TaskListId?: number;
  UserId: number;
}

export class Tasks extends React.Component<{}, List<ITaskEntity> {

  constructor(props: any) {
    super(props);

    this.loadTasks(this.props.match.params.id);
  }

  componentWillReceiveProps(newProps: any) {
    this.loadTasks(newProps.match.params.id);
  }

  loadTasks(id : number) {
    if (id) {
      request("/api/task/get?id=" + id).then(data => {
        this.setState({this.state.tasks = data})})
    } else {
      request("/api/task/get").then(data => {
        this.setState({ tasks: data });
      });
    }
  };

  taskOpen = (id: string) => {
    const task = document.getElementById(id);
    if (task.style.display === "none" || task.style.display.length === 0) {
      task.style.display = "block";
    } else {
      task.style.display = "none";
    }
  };

  taskDone = async id:number => {
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
                  onClick={() => this.taskOpen(task.id)}
                >
                  <div>
                    {task.name} (Expires on: {task.expiresOn})
                  </div>
                </div>
                <div className="task-card-body" id={task.id}>
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
