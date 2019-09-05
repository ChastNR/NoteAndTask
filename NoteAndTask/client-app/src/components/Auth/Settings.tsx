import React from "react";
import { DashBoard } from "../layout/DashBoard";

import { IUser } from "../../interfaces/IUser";
import { observer, inject } from "mobx-react";

@inject('userStore')
@observer
export class Settings extends React.Component<any, IUser> {
  constructor(props: any) {
    super(props);
    if (!!!this.props.userStore.name || this.props.userStore.email || this.props.userStore.phoneNumber) {
      this.props.userStore.getUser();
    }
  }

  render() {
    return (
      <DashBoard>
        {this.props.userStore.name}
        {this.props.userStore.email}
        {this.props.userStore.phoneNumber}
      </DashBoard>
    )
  }
}
