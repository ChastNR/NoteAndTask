import React from "react";
import { Button, ControlLabel, Form, FormGroup, HelpBlock, Modal } from "rsuite";
import { observer, inject } from "mobx-react";
import { thisTypeAnnotation } from "@babel/types";

interface IModalWindow {
  show: boolean
}

@inject('listsStore')
@observer
export class AddListModal extends React.Component<any, IModalWindow> {
  constructor(props: any) {
    super(props);

    this.state = {
      show: false
    };

    this.close = this.close.bind(this);
    this.open = this.open.bind(this);
  }

  close() {
    this.setState({ show: false });
  }
  open() {
    this.setState({ show: true });
  }

  handleSubmit = async (event: any) => {
    event.preventDefault();

    if (event.target.checkValidity()) {
      await fetch("api/list/add?name=" + event.target.name.value, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + localStorage.getItem("token")
        }
      });
      this.close();
      this.props.listsStore.loadLists();
    } else {
      event.target.reportValidity();
    }
  };

  render() {
    return (
      <div className="modal-container">
        <Button color="blue" appearance="ghost" onClick={this.open}>
          +
        </Button>
        <Modal show={this.state.show} onHide={this.close}>
          {/* <Modal.Header>
            <Modal.Title>Add new list</Modal.Title>
          </Modal.Header> */}
          <Form fluid onSubmit={this.handleSubmit}>
            <Modal.Body>
              <FormGroup>
                <ControlLabel>Name:</ControlLabel>
                <input className="rs-input" type="text" name="name" required />
                <HelpBlock>Required</HelpBlock>
              </FormGroup>
            </Modal.Body>
            <Modal.Footer>
              <Button onClick={this.close} appearance="subtle">
                Close
              </Button>
              <Button appearance="primary" type="submit">
                Add
              </Button>
            </Modal.Footer>
          </Form>
        </Modal>
      </div>
    );
  }
}
