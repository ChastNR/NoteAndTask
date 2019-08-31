import * as React from "react";
import { ITask } from "../../interfaces/ITask";
import { Divider, Button } from "rsuite";
import styled from "styled-components";

interface ITaskEntity {
    task: ITask
    expand: boolean
}

const TaskCard = styled.div`
  background-color: white;
  border-radius: 5px;
  padding: 10px;
  margin: 10px;

  box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.1), 0 1px 2px 0 rgba(0, 0, 0, 0.06);
`

const TaskCardBody = styled.div`
    display: none;
    margin-left: 5px;
`

export class Task extends React.Component<any, ITaskEntity> {
    constructor(props: any) {
        super(props)

        this.state = {
            task: this.props.task, expand: false
        }
    }

    taskOpen() {
        this.state.expand ?
            this.setState({ expand: false }) :
            this.setState({ expand: true })
    }

    taskDone = async (id: number) => {
        console.log(id)
        //  await request("/api/task/done?Id=" + id);
    };

    doneButton() {
        if (this.state.task.isDone !== true) {
            return (
                <div>
                    <Divider />
                    <Button onClick={() => this.taskDone(this.state.task.id)} color="blue">
                        Done
                        </Button>
                </div>
            )
        }
    }

    render() {
        return (
            <TaskCard key={this.state.task.id}>
                <div className="row align-items-center task-card-name" onClick={() => this.taskOpen()} >
                    <div>
                        {this.state.task.name} (Expires on: {this.state.task.expiresOn})
                  </div>
                </div>
                <TaskCardBody style={{ display: this.state.expand ? "block" : "none" }}>
                    <Divider>Description</Divider>
                    <div>{this.state.task.description}</div>
                    {this.doneButton()}
                </TaskCardBody>
            </TaskCard>
        )
    }
}