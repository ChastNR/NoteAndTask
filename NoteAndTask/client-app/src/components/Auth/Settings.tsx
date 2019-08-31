import React from "react";
import { DashBoard } from "../layout/DashBoard";
import { request } from "../../libs/api";

import { IUser } from "../../interfaces/IUser";

export class Settings extends React.Component<any, IUser> {
  static displayName = Settings.name;

  constructor(props: any) {
    super(props);
    this.state = {
      name: "",
      email: "",
      phoneNumber: ""
    };

    this.getUser();
  }

  getUser() {
    request("/api/account/getuser").then(data => {
      this.setState({
        name: data["name"],
        email: data["email"],
        phoneNumber: data["phoneNumber"]
      } as IUser);
    });
  }

  render() {
    return (
      <DashBoard>
        {this.state.name}
        {this.state.email}
        {this.state.phoneNumber}
      </DashBoard>
    );
  }
}
