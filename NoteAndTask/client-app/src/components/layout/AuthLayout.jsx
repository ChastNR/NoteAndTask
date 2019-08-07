import React from "react";
import { Link } from "react-router-dom";
import { Container, Navbar, NavbarBrand, NavItem, Row } from "react-bootstrap";
import "./AuthLayout.css";
import { BoardLayout } from "./BoardLayout";
import { request } from "../../libs/api";

export class AuthLayout extends React.Component {
  static displayName = AuthLayout.name;

  constructor(props) {
    super(props);
    this.state = {
      signInUpForm: null,
      user: null
    };

    this.getUser();
  }

  getUser() {
    if (this.state.user == null && localStorage.getItem("token")) {
      request("/api/account/getUser").then(data => {
        this.setState({ user: data["name"] });
      });
    }
  }

  renderAuthPanel() {
    if (localStorage.getItem("token")) {
      return (
        <div className="collapse navbar-collapse" id="navigation-toggler">
          <ul className="navbar-nav ml-auto">
            <div className="dropdown-divider" />
            <NavItem>
              {/*<Link to="/board">Board</Link>*/}
              <Link to="/dashboard">Board</Link>
            </NavItem>
            <div className="dropdown-divider" />
            <NavItem>
              <Link to="/settings">{this.state.user}</Link>
            </NavItem>
          </ul>
        </div>
      );
    } else {
      return (
        <div className="collapse navbar-collapse" id="navigation-toggler">
          <ul className="navbar-nav ml-auto">
            <div className="dropdown-divider" />
            <NavItem>
              <Link to="/SignIn">Sign In</Link>
            </NavItem>
            <div className="dropdown-divider" />
            <NavItem>
              <Link to="/SignUp">Sign Up</Link>
            </NavItem>
          </ul>
        </div>
      );
    }
  }

  render() {
    return (
      <div>
        <header id="mainHeader" className="fixed-top">
          <Container>
            <Navbar className="navbar-expand-md" id="main-nav">
              <NavbarBrand href="/">Note&Task</NavbarBrand>

              <a
                type="button"
                className="navbar-toggler"
                data-toggle="collapse"
                data-target="#navigation-toggler"
                aria-controls="navigation-toggler"
                aria-expanded="false"
                aria-label="Toggle navigation"
              >
                <i className="fas fa-bars" />
              </a>
              {this.renderAuthPanel()}
              {/*<div className="collapse navbar-collapse" id="navigation-toggler">*/}
              {/*    <ul className="navbar-nav ml-auto">*/}
              {/*        <div className="dropdown-divider"/>*/}
              {/*        <NavItem>*/}
              {/*            <Link to='/SignIn'>Sign In</Link>*/}
              {/*        </NavItem>*/}
              {/*        <div className="dropdown-divider"/>*/}
              {/*        <NavItem>*/}
              {/*            <Link to='/SignUp'>Sign Up</Link>*/}
              {/*        </NavItem>*/}
              {/*    </ul>*/}
              {/*</div>*/}
            </Navbar>
          </Container>
        </header>
        <div id="frontpage">
          <section id="home">
            <Container>
              <Row className="align-items-center">
                <div className="col-md-6 text-center">
                  <h1>Note&Task</h1>
                  <h4>
                    Welcome! This is best application for creating Tasks and
                    Notes to manage your time!
                  </h4>
                </div>
                {this.props.children}
              </Row>
            </Container>
          </section>
        </div>
      </div>
    );
  }
}