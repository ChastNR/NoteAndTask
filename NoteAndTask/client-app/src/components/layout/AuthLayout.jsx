import React from "react";
import { Link } from "react-router-dom";
import { Navbar, Nav } from "rsuite";
// import "./AuthLayout.css";

export class AuthLayout extends React.Component {
  static displayName = AuthLayout.name;

  constructor(props) {
    super(props);
    this.state = {
      signInUpForm: null
    };
  }

  renderAuthPanel() {
    if (localStorage.getItem("token")) {
      return (
        <Nav>
          <Nav.Item componentClass={Link} to="/dashboard">
            Board
          </Nav.Item>
        </Nav>
      );
    } else {
      return (
        <Nav>
          <Nav.Item componentClass={Link} to="/signin">
            Sign In
          </Nav.Item>
          <Nav.Item componentClass={Link} to="/signup">
            Sign Up
          </Nav.Item>
        </Nav>
      );
    }
  }

  render() {
    return (
      <div>
        <header>
          <Navbar>
            <Navbar.Header>
              <a className="navbar-brand logo" href="/">
                Note&Task
              </a>
            </Navbar.Header>
            <Navbar.Body>{this.renderAuthPanel()}</Navbar.Body>
          </Navbar>
        </header>
        <div>{this.props.children}</div>
      </div>
    );
  }
}
