import React from "react";
import { request } from "../../libs/api";
import { DashBoard } from "../layout/DashBoard";

import { ITask } from "../../interfaces/ITask";
import { Task } from "./Task";
import { observer, inject } from "mobx-react";

interface ITasks {
  tasks: ITask[];
}

@inject('tasksStore')
@observer
export class Archive extends React.Component<any, ITasks> {
  constructor(props: any) {
    super(props);

    this.props.tasksStore.loadTasks();
  }

  render() {
    return (
      <DashBoard>
        {this.props.tasksStore.archiveState && (
          <div>
            {this.props.tasksStore.archiveState.map((task: ITask) => (
              <Task task={task} />
            ))}
          </div>
        )}
      </DashBoard>
    );
  }
}
