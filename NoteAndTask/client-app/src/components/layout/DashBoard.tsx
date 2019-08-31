import * as React from "react";
import { Navbar, Nav } from "rsuite";
import { Link } from "react-router-dom";
import { Footer } from "./Footer";

export class DashBoard extends React.Component {
  constructor(props: any) {
    super(props);
  }

  render() {
    return (
      <div>
        <header>
          <Navbar appearance="subtle" style={{ boxShadow: "0 1px 3px 0 rgba(0, 0, 0, 0.1), 0 1px 2px 0 rgba(0, 0, 0, 0.06);" }}>
            <Navbar.Body>
              <Nav>
                <Nav.Item componentClass={Link} to="/lists">
                  Lists
                </Nav.Item>
                <Nav.Item componentClass={Link} to="/tasks">
                  Tasks
                </Nav.Item>
                <Nav.Item componentClass={Link} to="/notes">
                  Notes
                </Nav.Item>
                <Nav.Item componentClass={Link} to="/archive">
                  Archive
                </Nav.Item>
              </Nav>
              <Nav pullRight>
                <Nav.Item componentClass={Link}>
                  <i className="fas fa-search" />
                </Nav.Item>
                <Nav.Item componentClass={Link}>
                  <i className="far fa-bell" />
                </Nav.Item>
                <Nav.Item componentClass={Link} to="/settings">
                  <i className="fas fa-cog" />
                </Nav.Item>
              </Nav>
            </Navbar.Body>
          </Navbar>
        </header>
        <div className="main-content">{this.props.children}</div>
        <Footer />
      </div>
    );
  }
}
