import React from "react";
import { DashBoard } from "./layout/DashBoard";
import { request } from "../libs/api";
import "./Settings.css";

export class Settings extends React.Component {
  static displayName = Settings.name;

  constructor(props) {
    super(props);
    this.state = {
      user: null,
      email: null,
      phoneNumber: null
    };
    this.getUser();
  }

  getUser() {
    request("/api/account/getuser").then(data => {
      this.setState({
        user: data["name"],
        email: data["email"],
        phoneNumber: data["phoneNumber"]
      });
    });
  }

  render() {
    return (
      <DashBoard>
        <div className="m-3">
          <table class="table-fill">
            <thead>
              <tr>
                <th class="text-left">Title</th>
                <th class="text-left">Value</th>
              </tr>
            </thead>
            <tbody class="table-hover">
              <tr>
                <td class="text-left">Name</td>
                <td class="text-left">{this.state.user}</td>
              </tr>
              <tr>
                <td class="text-left">Email</td>
                <td class="text-left">{this.state.email}</td>
              </tr>
              <tr>
                <td class="text-left">Phone number</td>
                <td class="text-left">{this.state.phoneNumber}</td>
              </tr>
            </tbody>
          </table>
        </div>
      </DashBoard>
    );
  }
}
