import React from 'react';
import {request} from "../libs/api";
import {BoardLayout} from "./layout/BoardLayout";
import {DashBoard} from "./layout/DashBoard";
import "./Tasks.css";
import {AddNewTaskModal} from "./layout/AddNewTaskModal";

export function updateState(id) {
    if (id) {
        request('/api/Task/Tasks?Id=' + id)
            .then(data => {
                Tasks.setState({tasks: data})
            });
    } else {
        request('/api/Task/Tasks')
            .then(data => {
                Tasks.setState({tasks: data})
            });
    }
}

export class Tasks extends React.Component {
    static displayName = Tasks.name;

    constructor(props) {
        super(props);
        this.state = {tasks: null, done: null, visible: false};

        this.loadTasks(this.props.match.params.id);
    }

    loadTasks(id) {
        if (id) {
            request('/api/Task/Tasks?Id=' + id)
                .then(data => {
                    this.setState({tasks: data})
                });
        } else {
            request('/api/Task/Tasks')
                .then(data => {
                    this.setState({tasks: data})
                });
        }
    }

   
    
    componentWillReceiveProps(newProps) {
        this.loadTasks(newProps.match.params.id);
    }

    taskOpen = (id) => {
        const task = document.getElementById(id);
        if (task.style.display === "none" || task.style.display.length === 0) {
            task.style.display = "block";
        } else {
            task.style.display = "none";
        }
    };

    taskDone = async (id) => {
        await fetch('/api/Task/TaskDone?Id=' + id, {
            headers: {
                "Content-Type": "application/json",
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            },
        }).then(response => response.json())
            .then(() => this.loadTasks())
    };

    render() {
        return (
            <DashBoard>
                <div className="task-card shadow-sm">
                    <div className="row align-items-center task-card-name">
                        {/*<a href="#" onClick={() => this.addNewTask} >Add new task</a>*/}
                        <AddNewTaskModal/>
                    </div>
                </div>
                {this.state.tasks &&
                <div>
                    {this.state.tasks.map(task =>
                        <div className="task-card shadow-sm" key={task.taskId}>
                            <div className="row align-items-center task-card-name"
                                 onClick={() => this.taskOpen(task.taskId)}>
                                <div>
                                    {task.name} (Expires on: {task.expiresOn})
                                </div>
                            </div>
                            <div className="task-card-body" id={task.taskId}>
                                <hr/>
                                <div>
                                    {task.description}
                                </div>
                                <hr/>
                                <div className="card-done-button">
                                    <a href="#" onClick={() => this.taskDone(task.taskId)}>
                                        Done
                                    </a>
                                </div>
                            </div>
                        </div>
                    )}
                </div>
                }
            </DashBoard>
        );
    }
}