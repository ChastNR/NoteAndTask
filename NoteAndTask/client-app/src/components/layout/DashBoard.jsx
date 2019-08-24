import React from "react";
import lodash from "lodash";
import { Link } from "react-router-dom";
import "./Dashboard.css";
import { Footer } from "./Footer";
import brandLogo from "../../images/brand-icon.png";

export class DashBoard extends React.Component {
  static displayName = DashBoard.name;

  // handleClick = (e) => {
  //   e.preventDefault();
  //   return lodash.debounce( ,1000);
  // }
  //
  //
  //
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
                </li>
                <li className="nav-item">
                  <Link to="/archive" className="nav-link">
                    Archive
                  </Link>
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
                    <span className="mr-2">My Profile</span>
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
