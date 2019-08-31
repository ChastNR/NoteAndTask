import React from "react";
import { request } from "../../libs/api";
import { DashBoard } from "../layout/DashBoard";

import { ITask } from "../../interfaces/ITask";
import { Task } from "./Task";

interface ITasks {
  tasks: ITask[];
}

export class Archive extends React.Component<any, ITasks> {
  constructor(props: any) {
    super(props);

    this.state = {
      tasks: []
    };

    this.loadTasks();
  }

  loadTasks() {
    request("/api/task/get?archived=true").then(data => {
      this.setState({ tasks: data } as ITasks);
    });
  }

  render() {
    return (
      <DashBoard>
        {this.state.tasks && (
          <div>
            {this.state.tasks.map(task => (
              <Task task={task} />
            ))}
          </div>
        )}
      </DashBoard>
    );
  }
}
