import React from "react";
import { Link } from "react-router-dom";
import "./Footer.css";

export class Footer extends React.Component {
  static displayName = Footer.name;

  render() {
    return (
      <footer>
        <div>
          <nav className="navbar">
            <div className="align-items-center">
              2019&nbsp;©&nbsp;<Link>Note & Task</Link>
            </div>
            <div>
              <ul className="footer-links">
                <li>
                  <Link>About</Link>
                </li>
                <li>
                  <Link>Team</Link>
                </li>
                <li>
                  <Link>Privacy</Link>
                </li>
              </ul>
            </div>
          </nav>
        </div>
      </footer>
    );
  }
}
