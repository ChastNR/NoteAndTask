import React from "react";
import { Modal, Button, DatePicker, Alert, ControlLabel, Form, FormGroup, HelpBlock } from "rsuite";
import { request } from "../../libs/api";
import { IList } from "../../interfaces/IList";

interface ITaskModalWindow {
  show: boolean
  lists: IList[]
}

export class AddNewTaskModal extends React.Component<any, ITaskModalWindow> {
  constructor(props: any) {
    super(props);
    this.state = { show: false, lists: [] };
    this.close = this.close.bind(this);
    this.open = this.open.bind(this);
  }

  close() {
    this.setState({ show: false });
  }
  open() {
    this.setState({ show: true });
  }

  loadLists() {
    request("/api/list/get").then(data => {
      this.setState({ lists: data })
      console.log(data)
    });
  }

  handleSubmit = async (event: any) => {
    event.preventDefault();

    if (event.target.checkValidity()) {
      let formData;

      if (event.target.listId.value.length === 0) {
        formData = {
          name: event.target.name.value,
          description: event.target.description.value,
          expiresOn: event.target.expiresOn.value
        };
      } else {
        formData = {
          name: event.target.name.value,
          description: event.target.description.value,
          expiresOn: event.target.expiresOn.value,
          taskListId: event.target.listId.value
        };
      }

      await fetch("api/task/add", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + localStorage.getItem("token")
        },
        body: JSON.stringify(formData)
      });
      this.close();
      Alert.info("New task added", 3000);
    } else {
      event.target.reportValidity();
    }
  };

  render() {
    return (
      <div className="modal-container">
        <Button color="blue" appearance="ghost" onClick={() => { this.open(); this.loadLists(); }} >
          +
        </Button>
        <Modal show={this.state.show} onHide={this.close}>
          {/* <Modal.Header>
            <Modal.Title>Add new task</Modal.Title>
          </Modal.Header> */}
          <Form fluid onSubmit={this.handleSubmit}>
            <Modal.Body>
              <FormGroup>
                <ControlLabel>Name:</ControlLabel>
                <input className="rs-input" type="text" name="name" required />
                <HelpBlock>Required</HelpBlock>
              </FormGroup>
              <FormGroup>
                <ControlLabel>Description:</ControlLabel>
                <textarea className="rs-input" name="description" required />
                <HelpBlock>Required</HelpBlock>
              </FormGroup>
              <FormGroup>
                <ControlLabel>Expires on?</ControlLabel>
                <DatePicker type="datetime-local" format="YYYY-MM-DD HH:mm" required block name="expiresOn" ranges={[
                  {
                    label: "Now",
                    value: new Date()
                  }
                ]}
                />
                <HelpBlock>Required</HelpBlock>
              </FormGroup>
              {this.state.lists.length > 0 ? (
                <FormGroup>
                  <ControlLabel>Choose Task List:</ControlLabel>
                  <select name="listId">
                    {this.state.lists.map(list => (
                      <option value={list.id}>{list.name}</option>
                    ))}
                  </select>
                </FormGroup>
              ) : null}
            </Modal.Body>
            <Modal.Footer>
              <Button onClick={this.close} appearance="subtle">
                Close
              </Button>
              <Button type="submit" appearance="primary">
                Add
              </Button>
            </Modal.Footer>
          </Form>
        </Modal>
      </div>
    );
  }
}