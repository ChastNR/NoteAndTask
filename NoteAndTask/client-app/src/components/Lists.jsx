import React from "react";
import { DashBoard } from "./layout/DashBoard";
import { request } from "../libs/api";
import { Link } from "react-router-dom";
import "./css/global.css";
import "./Lists.css";
import { AddListModal } from "./layout/AddListModal";

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
    })
  }

  deleteTaskList = id => {
    request("/api/list/delete?id=" + id).then();
    this.loadLists();
  }

  // componentWillReceiveProps(newProps) {
  //   this.loadLists(newProps)
  // }
  
  render() {
    return (
      <DashBoard>
        <div className="task-card shadow-sm">
          <div className="row align-items-center task-card-name">
            <AddListModal />
          </div>
        </div>
        {this.state.lists && (
          <div>
            {this.state.lists.map(list => (
              <div className="task-card shadow-sm">
                <div className="task-card-name" key={list.id}>
                  <Link
                    className="link-button mr-3"
                    to={{ pathname: "/tasks/" + list.id }}
                  >
                    {list.name}
                  </Link>
                  <button
                    type="button"
                    className="link-button"
                    onClick={() => this.deleteTaskList(list.id)}
                  >
                    <i className="fas fa-backspace" />
                  </button>
                </div>
              </div>
            ))}
          </div>
        )}
      </DashBoard>
    );
  }
}
