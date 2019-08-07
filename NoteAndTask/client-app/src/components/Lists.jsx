import React from 'react';
import {DashBoard} from "./layout/DashBoard";
import {request} from "../libs/api";
import {Link} from "react-router-dom";
import "./Lists.css";
import {AddListModal} from "./layout/AddListModal";

export class Lists extends React.Component {
    static displayName = Lists.name;

    constructor(props) {
        super(props);
        this.state = {
            lists: null
        };

        this.loadLists();
    }

    loadLists() {
        if (this.state.lists == null) {
            request("/api/Task/Lists").then(data => {
                this.setState({lists: data});
            });
        }
    }

    deleteTaskList(id) {

    };

    render() {
        return (
            <DashBoard className="row list-view">
                <AddListModal/>
                {this.state.lists &&
                <div>
                    {this.state.lists.map(list =>
                        <div className="task-list" key={list.taskListId}>
                            <div className="btn-group">
                                <Link className="mr-3" to={{pathname: "/tasks/" + list.taskListId}}>
                                    {list.name}
                                </Link>
                                <a href="#" onClick={() => this.deleteTaskList(list.taskListId)}>
                                    <i className="fas fa-backspace"/>
                                </a>
                            </div>
                        </div>
                    )}
                </div>
                }
            </DashBoard>
        );
    }
}