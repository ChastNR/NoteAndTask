import React from "react";

import { request } from "../../libs/api";
import { DashBoard } from "../layout/DashBoard";
import { AddNewTaskModal } from "./AddNewTaskModal";

import { ITask } from "../../interfaces/ITask";
import { Task } from "./Task";
import styled from "styled-components";

interface ITasks {
  tasks: ITask[];
}

const TaskCard = styled.div`
  background-color: white;
  border-radius: 5px;
  padding: 10px;
  margin: 10px;

  box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.1), 0 1px 2px 0 rgba(0, 0, 0, 0.06);
`

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

  render() {
    return (
      <DashBoard>
        <TaskCard>
          <AddNewTaskModal />
        </TaskCard>
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
