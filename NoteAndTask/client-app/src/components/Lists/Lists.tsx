import * as React from "react";
import { DashBoard } from "../layout/DashBoard";
import { request } from "../../libs/api";
import { AddListModal } from "./AddListModal";

import { List } from "./List";
import { IList } from "../../interfaces/IList";
import styled from "styled-components";

interface ILists {
  lists: IList[];
}

const Card = styled.div`
  background-color: white;
  border-radius: 5px;
  padding: 10px;
  margin: 10px;

  box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.1), 0 1px 2px 0 rgba(0, 0, 0, 0.06);
`

export class Lists extends React.Component<any, ILists> {
  constructor(props: any) {
    super(props);

    this.state = {
      lists: []
    };

    this.loadLists();
  }

  loadLists() {
    request("/api/list/get").then(data => {
      this.setState({ lists: data } as ILists);
    });
  }

  render() {
    return (
      <DashBoard>
        <Card>
          <div className="task-card-name">
            <AddListModal />
          </div>
        </Card>
        {this.state.lists && (
          <div>
            {this.state.lists.map(list => (
              <List list={list} />
            ))}
          </div>
        )}
      </DashBoard>
    );
  }
}
