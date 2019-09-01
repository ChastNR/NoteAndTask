import * as React from "react";
import { Link } from "react-router-dom";
import { Navbar, Nav } from "rsuite";

const renderAuthPanel: any = () => {
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
};

export const AuthLayout: React.FC = (props) => {
  return (
    <div>
      <header>
        <Navbar>
          <Navbar.Header>
            <a className="navbar-brand logo" href="/">
              Note&Task
              </a>
          </Navbar.Header>
          <Navbar.Body>{renderAuthPanel()}</Navbar.Body>
        </Navbar>
      </header>
      <div>{props.children}</div>
    </div>
  )
};
