import React from "react";
import { Navbar, Nav } from "rsuite";
import { Link } from "react-router-dom";
import lodash from "lodash";
import { Footer } from "./Footer";
import "../../styles/default.css";

export class DashBoard extends React.Component {
  static displayName = DashBoard.name;

  handleClick = lodash.debounce(() => {}, 1000);

  render() {
    return (
      <div>
        <header>
          <Navbar appearance="subtle" className="shadow-box">
            <Navbar.Body>
              <Nav>
                <Nav.Item componentClass={Link} to="/lists">
                  Lists
                </Nav.Item>
                <Nav.Item
                  onSelect={() => this.handleClick}
                  componentClass={Link}
                  to="/tasks"
                >
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
