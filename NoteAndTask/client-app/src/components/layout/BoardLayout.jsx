import React from "react";
import {request} from "../../libs/api";
import {Link} from "react-router-dom";
import {Accordion, Button} from "react-bootstrap";
import Dropdown from "react-bootstrap/Dropdown";
import "./BoardLayout.css";
import {AddNewList} from "./AddNewListModal";
import {AddNewTaskModal} from "./AddNewTaskModal";

export class BoardLayout extends React.Component {
    static displayName = BoardLayout.name;

    constructor(props) {
        super(props);
        this.state = {
            user: null,
            lists: null
        };

        this.getUser();
    }

    getUser() {
        if (this.state.user === null) {
            request("/api/account/getUser").then(data => {
                this.setState({user: data["name"]});
            });
        }
    }

    loadLists() {
        if (this.state.lists == null) {
            request("/api/Task/Lists").then(data => {
                this.setState({lists: data});
            });
        }
    }

    handleClick = () => {
        let wrapper = document.getElementById("wrapper");
        if (wrapper.classList.contains("toggled")) {
            wrapper.classList.remove("toggled");
        } else {
            wrapper.classList.add("toggled");
        }
    };

    static logOut() {
        localStorage.removeItem("token");
        window.location.href = "/";
    }

    render() {
        return (
            <div className="d-flex" id="wrapper">
                <div className="bg-light border-right" id="sidebar-wrapper">
                    <div className="sidebar-heading">
                        <i className="fas fa-columns mr-3"/>
                        <Link to="/board">Tasks Board</Link>
                    </div>
                    <div className="list-group list-group-flush">
                        <div>
                            <Accordion>
                                <Accordion.Toggle
                                    as={Button}
                                    onClick={() => this.loadLists()}
                                    className="list-group-item list-group-item-action bg-light"
                                    variant="link"
                                    eventKey="0"
                                >
                                    <i className="far fa-list-alt mr-3"/>
                                    Lists
                                </Accordion.Toggle>
                                <Accordion.Collapse id="accordionCollapse" eventKey="0">
                                    <div>
                                        {this.state.lists && (
                                            <div>
                                                {this.state.lists.map(list => (
                                                    <div className="m-3" key={list.taskListId}>
                                                        <Link
                                                            to={{pathname: "/tasks/" + list.taskListId}}
                                                        >
                                                            <i className="fas fa-list-ol mr-3"/>
                                                            {list.name}
                                                        </Link>
                                                    </div>
                                                ))}
                                            </div>
                                        )}
                                        <div className="m-3">
                                            <AddNewList/>
                                        </div>
                                    </div>
                                </Accordion.Collapse>
                            </Accordion>
                        </div>
                        <div className="btn-group">
                            <Link
                                to="/tasks"
                                className="list-group-item list-group-item-action bg-light"
                            >
                                <i className="fas fa-tasks mr-3"/>
                                Tasks
                            </Link>
                            <AddNewTaskModal/>
                        </div>
                        <Link
                            to="/notes"
                            className="list-group-item list-group-item-action bg-light"
                        >
                            <i className="fas fa-archive mr-3"/>
                            Notes
                        </Link>

                        <Link
                            to="/archive"
                            className="list-group-item list-group-item-action bg-light"
                        >
                            <i className="fas fa-archive mr-3"/>
                            Archive
                        </Link>
                    </div>
                </div>
                <div id="page-content-wrapper">
                    <nav className="navbar navbar-expand-lg navbar-light bg-light border-bottom">
                        <button
                            className="btn btn-primary"
                            onClick={this.handleClick}
                            id="menu-toggle"
                        >
                            <i className="fas fa-bars"/>
                        </button>
                        <Dropdown className="ml-auto" drop="left">
                            <Dropdown.Toggle variant={"link"}>
                                {this.state.user}
                            </Dropdown.Toggle>
                            <Dropdown.Menu className="dropdown-menu-right">
                                <Dropdown.Item>
                                    <Link to="/settings">
                                        <i className="fas fa-user-cog mr-2"/>
                                        Settings
                                    </Link>
                                </Dropdown.Item>
                                <Dropdown.Divider/>
                                <Dropdown.Item>
                                    <i className="fas fa-sign-out-alt mr-2"/>
                                    <a onClick={() => BoardLayout.logOut()}>Logout</a>
                                </Dropdown.Item>
                            </Dropdown.Menu>
                        </Dropdown>
                    </nav>
                    <div className="container-fluid">{this.props.children}</div>
                </div>
            </div>
        );
    }
}
