import React from "react";
import { request } from "../../libs/api";
import { Link } from "react-router-dom";
import "./Dashboard.css";
import { Footer } from "./Footer";
import brandLogo from "../../images/brand-icon.png";

export class DashBoard extends React.Component {
  static displayName = DashBoard.name;

  constructor(props) {
    super(props);
    this.state = {
      user: null
    };
  
    if(this.state.user === null)
    {
      this.getUser();
    }
    
  }

  getUser() {
      request("/api/account/getuser").then(data => {
        this.setState({ user: data["name"] });
      });
  }

  render() {
    return (
      <div>
        <header>
          <nav className="navbar shadow-sm fixed">
            <div>
              <ul className="nav align-items-center">
                <li className="nav-item">
                  <Link id="test" className="nav-link">
                    <i className="fas fa-bars" />
                  </Link>
                </li>
                <li className="nav-item">
                  <Link to="/lists" className="nav-link">
                    Lists
                  </Link>
                </li>
                <li className="nav-item">
                  <Link to="/tasks" className="nav-link">
                    Tasks
                  </Link>
                </li>
                <li className="nav-item">
                  <Link to="/notes" className="nav-link">
                    Notes
                  </Link>
                  {/*<a className="nav-link" href="#">*/}
                  {/*    Notes*/}
                  {/*</a>*/}
                </li>
                <li className="nav-item">
                  <Link to="/archive" className="nav-link">
                    Archive
                  </Link>
                  {/*<a className="nav-link" href="#">*/}
                  {/*    Archive*/}
                  {/*</a>*/}
                </li>
              </ul>
            </div>
            <Link className="navbar-brand" to="/">
              <img src={brandLogo} height="40" alt="Logo" />
            </Link>
            <div>
              <ul className="nav align-items-center">
                <li className="nav-item">
                  <Link className="nav-link">
                    <i className="fas fa-search" />
                  </Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link">
                    <i className="far fa-bell" />
                  </Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link">
                    <i className="fas fa-cog" />
                  </Link>
                </li>
                <li className="nav-item">
                  <Link to="/settings" className="nav-link">
                    <span className="mr-2">{this.state.user}</span>
                    {/*<img src="../../images/user-icon.png" height="32" alt="User icon"*/}
                    {/*/>*/}
                  </Link>
                </li>
              </ul>
            </div>
          </nav>
        </header>
        <div className="main-content">{this.props.children}</div>
        <Footer />
      </div>
    );
  }
}
