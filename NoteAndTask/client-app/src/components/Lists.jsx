import React from "react";
import { DashBoard } from "./layout/DashBoard";
import { request } from "../libs/api";
import { Link } from "react-router-dom";
import { AddListModal } from "./layout/AddListModal";
import "../styles/default.css";
import { Button } from "rsuite";

export class Lists extends React.Component {
  static displayName = Lists.name;

  constructor(props) {
    super(props);
    this.state = {
      lists: null
    };

    this.loadLists();
  }

  loadLists() {
    request("/api/list/get").then(data => {
      this.setState({ lists: data });
    });
  }

  deleteTaskList = id => {
    request("/api/list/delete?id=" + id).then();
    this.loadLists();
  };

  // componentWillReceiveProps(newProps) {
  //   this.loadLists(newProps)
  // }

  render() {
    return (
      <DashBoard>
        <div className="task-card shadow-box">
          <div className="task-card-name">
            <AddListModal />
          </div>
        </div>
        {this.state.lists && (
          <div>
            {this.state.lists.map(list => (
              <div className="task-card shadow-box">
                <div key={list.id}>
                  <Button
                    componentClass={Link}
                    appearance="link"
                    style={{ fontSize: "1.1em" }}
                    to={{ pathname: "/tasks/" + list.id }}
                  >
                    {list.name}
                  </Button>
                  <Button
                    appearance="link"
                    style={{ color: "red" }}
                    onClick={() => this.deleteTaskList(list.id)}
                  >
                    Delete
                  </Button>
                </div>
              </div>
            ))}
          </div>
        )}
      </DashBoard>
    );
  }
}
