import * as React from "react";
import { DashBoard } from "../layout/DashBoard";
import { AddListModal } from "./AddListModal";

import { List } from "./List";
import { IList } from "../../interfaces/IList";
import styled from "styled-components";
import { observer, inject } from "mobx-react";

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

@inject('listsStore')
@observer
export class Lists extends React.Component<any, ILists> {
  constructor(props: any) {
    super(props);

    this.props.listsStore.loadLists();
  }
  render() {
    return (
      <DashBoard>
        <Card>
          <div className="task-card-name">
            <AddListModal />
          </div>
        </Card>
        {this.props.listsStore.lists && (
          <div>
            {this.props.listsStore.lists.map((list: IList) => (
              <List list={list} />
            ))}
          </div>
        )}
      </DashBoard>
    )
  }
}
