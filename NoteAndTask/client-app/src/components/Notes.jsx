import React from "react";
import { SketchField, Tools } from "react-sketch";
import "./Notes.css";
import { DashBoard } from "./layout/DashBoard";

export class Notes extends React.Component {
  static displayName = Notes.name;

  constructor(props) {
    super(props);
    this.state = { notes: null, loading: false };
  }

  render() {
    return (
      <DashBoard>
        <SketchField
          width="1024px"
          height="768px"
          tool={Tools.Pencil}
          lineColor="black"
          lineWidth={1}
        />
      </DashBoard>
    );
  }
}
