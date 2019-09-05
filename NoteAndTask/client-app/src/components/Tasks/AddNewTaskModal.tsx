import React from "react";
import { Modal, Button, DatePicker, Alert, ControlLabel, Form, FormGroup, HelpBlock } from "rsuite";
import { request } from "../../libs/api";
import { IList } from "../../interfaces/IList";
import { inject, observer } from "mobx-react";

interface ITaskModalWindow {
  show: boolean
}

@inject('applicationStore')
@observer
export class AddNewTaskModal extends React.Component<any, ITaskModalWindow> {
  constructor(props: any) {
    super(props);
    this.state = { show: false };
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

    if (!event.target.checkValidity()) {
      event.target.reportValidity();
    } else {
      let formData;

      //event.target.expiresOn.value;


      if (event.target.listId === undefined) {
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

      this.props.applicationStore.loadTasks();

      Alert.info("New task added", 3000);
    }
  };

  render() {
    return (
      <div className="modal-container">
        <Button color="blue" appearance="ghost" onClick={() => { this.open(); }} >
          +
        </Button>
        <Modal show={this.state.show} onHide={this.close}>
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
                <input type="datetime-local" name="expiresOn" required />
                {/*<DatePicker type="datetime-local" format="YYYY-MM-DD HH:mm" id="dateField" required block ranges={[*/}
                {/*  {*/}
                {/*    label: "Now",*/}
                {/*    value: new Date()*/}
                {/*  }*/}
                {/*]}*/}
                {/*/>*/}
                <HelpBlock>Required</HelpBlock>
              </FormGroup>
              {this.props.applicationStore.lists.length > 0 ? (
                <FormGroup>
                  <ControlLabel>Choose Task List:</ControlLabel>
                  <select name="listId">
                    {this.props.applicationStore.lists.map((list: any) => (
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