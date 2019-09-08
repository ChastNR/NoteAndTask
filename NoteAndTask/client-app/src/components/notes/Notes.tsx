import * as React from "react";
import { DashBoard } from "../layout/DashBoard";

export class Notes extends React.Component {
  static displayName = Notes.name;

  constructor(props: any) {
    super(props);
  }

  render() {
    return (
      <DashBoard>
        Notes
      </DashBoard>
    );
  }
}
