import React from "react";
import {request} from "../../libs/api";
import {Link} from "react-router-dom";
import {Accordion, Button} from "react-bootstrap";
import Dropdown from "react-bootstrap/Dropdown";
import "./Dashboard.css";
import {Footer} from "./Footer";
import brandLogo from "../../images/brand-icon.png";

export class DashBoard extends React.Component {
    static displayName = DashBoard.name;

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
            <div>
                <header>
                    <nav className="navbar shadow-sm fixed">
                        <div>
                            <ul className="nav align-items-center">
                                <li className="nav-item">
                                    <a id="test" className="nav-link" href="#">
                                        <i className="fas fa-bars" />
                                    </a>
                                </li>
                                <li className="nav-item">
                                    <Link to="/lists" className="nav-link" >
                                        Lists
                                    </Link>
                                </li>
                                <li className="nav-item">
                                    <Link to="/tasks" className="nav-link" >
                                        Tasks
                                    </Link>
                                </li>
                                <li className="nav-item">
                                    <Link to="/notes" className="nav-link" >
                                        Notes
                                    </Link>
                                    {/*<a className="nav-link" href="#">*/}
                                    {/*    Notes*/}
                                    {/*</a>*/}
                                </li>
                                <li className="nav-item">
                                    <Link to="/archive" className="nav-link" >
                                        Archive
                                    </Link>
                                    {/*<a className="nav-link" href="#">*/}
                                    {/*    Archive*/}
                                    {/*</a>*/}
                                </li>
                            </ul>
                        </div>
                        <Link className="navbar-brand" to="/"
                        ><img src={brandLogo} height="40" alt="Logo"
                        /></Link>
                        <div>
                            <ul className="nav align-items-center">
                                <li className="nav-item">
                                    <a className="nav-link" href="#"><i className="fas fa-search" /></a>
                                </li>
                                <li className="nav-item">
                                    <a className="nav-link" href="#"><i className="far fa-bell" /></a>
                                </li>
                                <li className="nav-item">
                                    <a className="nav-link" href="#"><i className="fas fa-cog" /></a>
                                </li>
                                <li className="nav-item">
                                    <a className="nav-link" href="#"
                                    ><span className="mr-2">Tihon</span>
                                        {/*<img src="../../images/user-icon.png" height="32" alt="User icon"*/}
                                        {/*/>*/}
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </nav>
                </header>
                <div className="main-content">
                    {this.props.children}
                </div>
                <Footer />
            </div>
        );
    }
}