import React from "react";
import { observer, inject } from "mobx-react";

import { DashBoard } from "../layout/DashBoard";
import { AddNewTaskModal } from "./AddNewTaskModal";

import { ITask } from "../../interfaces/ITask";
import { Task } from "./Task";
import styled from "styled-components";

const TaskCard = styled.div`
  background-color: white;
  border-radius: 5px;
  padding: 10px;
  margin: 10px;

  box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.1), 0 1px 2px 0 rgba(0, 0, 0, 0.06);
`

interface ITasks {
  tasks: ITask[]
}

@inject('tasksStore')
@observer
export class Tasks extends React.Component<any, ITasks> {

  constructor(props: ITasks) {
    super(props);

    this.props.tasksStore.loadTasks();
  }

  // componentWillReceiveProps(newProps: any) {
  //   this.props.tasksStore.tasks.filter((task: ITask) => task.taskListId == newProps);
  // }

  render() {
    return (
      <DashBoard>
        <TaskCard>
          <AddNewTaskModal />
        </TaskCard>
        {this.props.tasksStore.tasks && (
          <div>
            {this.props.tasksStore.tasks.map((task: ITask) => (
              <Task task={task} />
            ))}
          </div>
        )}
      </DashBoard>
    );
  }
}
