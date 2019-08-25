import React from "react";
import {
  Modal,
  Button,
  SelectPicker,
  DatePicker,
  Alert,
  ControlLabel,
  Form,
  FormControl,
  FormGroup,
  HelpBlock
} from "rsuite";
import { request } from "../../libs/api";

export class AddNewTaskModal extends React.Component {
  static displayName = AddNewTaskModal.name;

  constructor(props) {
    super(props);
    this.state = {
      show: false,
      lists: null
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

  loadLists() {
    if (this.state.lists == null) {
      request("/api/list/get").then(data => {
        this.setState({ lists: data });
      });
    }
  }

  handleSubmit = async event => {
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
        <Button
          color="blue"
          appearance="ghost"
          onClick={() => {
            this.open();
            this.loadLists();
          }}
        >
          Add new task
        </Button>

        <Modal show={this.state.show} onHide={this.close}>
          <Modal.Header>
            <Modal.Title>Add new task</Modal.Title>
          </Modal.Header>
          <Form fluid onSubmit={this.handleSubmit}>
            <Modal.Body>
              <FormGroup>
                <ControlLabel>Name:</ControlLabel>
                <FormControl type="text" name="name" required />
                <HelpBlock>Required</HelpBlock>
              </FormGroup>
              <FormGroup>
                <ControlLabel>Description:</ControlLabel>
                <FormControl
                  name="description"
                  componentClass="textarea"
                  rows="4"
                  cols="50"
                  required
                />
                <HelpBlock>Required</HelpBlock>
              </FormGroup>
              <FormGroup>
                <ControlLabel>Expires on?</ControlLabel>
                <DatePicker
                  type="datetime-local"
                  format="YYYY-MM-DD HH:mm"
                  required
                  block
                  name="expiresOn"
                  ranges={[
                    {
                      label: "Now",
                      value: new Date()
                    }
                  ]}
                />
                <HelpBlock>Required</HelpBlock>
                {/*<input*/}
                {/*  name="expiresOn"*/}
                {/*  className="form-control"*/}
                {/*  type="datetime-local"*/}
                {/*  required*/}
                {/*/>*/}
              </FormGroup>
              <FormGroup>
                <ControlLabel>Choose Task List:</ControlLabel>
                {this.state.lists && (
                  <SelectPicker
                    name="listId"
                    searchable={false}
                    block
                    data={this.state.lists}
                  ></SelectPicker>
                )}
              </FormGroup>
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
