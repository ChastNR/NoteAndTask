import * as React from "react";
import { request } from "../../libs/api";
import { Link } from "react-router-dom";
import { Button } from "rsuite";

import { IList } from "../../interfaces/IList";
import styled from "styled-components";

interface ILists {
    list: IList;
}

const Card = styled.div`
  background-color: white;
  border-radius: 5px;
  padding: 10px;
  margin: 10px;

  box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.1), 0 1px 2px 0 rgba(0, 0, 0, 0.06);
`

export class List extends React.Component<ILists, ILists> {
    constructor(props: ILists) {
        super(props);

        this.state = {
            list: this.props.list
        };
    }

    deleteTaskList = (id: number) => {
        request("/api/list/delete?id=" + id);
    };

    render() {
        return (
            <Card key={this.state.list.id}>
                <Button
                    componentClass={Link}
                    appearance="link"
                    style={{ fontSize: "1.1em" }}
                    to={{ pathname: "/tasks/" + this.state.list.id }}
                >
                    {this.state.list.name}
                </Button>
                <Button
                    appearance="link"
                    style={{ color: "red" }}
                    onClick={() => this.deleteTaskList(this.state.list.id)}
                >
                    Delete
                  </Button>
            </Card>

        );
    }
}
