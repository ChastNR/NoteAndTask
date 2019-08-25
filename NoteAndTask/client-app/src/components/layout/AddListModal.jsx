import React from "react";
import {
  Button,
  ControlLabel,
  Form,
  FormControl,
  FormGroup,
  HelpBlock,
  Modal
} from "rsuite";

export class AddListModal extends React.Component {
  static displayName = AddListModal.name;

  constructor(props) {
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

  handleSubmit = async event => {
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
    } else {
      event.target.reportValidity();
    }
  };

  render() {
    return (
      <div className="modal-container">
        <Button color="blue" appearance="ghost" onClick={this.open}>
          Add new list
        </Button>
        <Modal show={this.state.show} onHide={this.close}>
          <Modal.Header>
            <Modal.Title>Add new list</Modal.Title>
          </Modal.Header>
          <Form fluid onSubmit={this.handleSubmit}>
            <Modal.Body>
              <FormGroup>
                <ControlLabel>Name:</ControlLabel>
                <FormControl type="text" name="name" />
                <HelpBlock>Required</HelpBlock>
              </FormGroup>
            </Modal.Body>
            <Modal.Footer>
              <Button onClick={this.close} appearance="subtle">
                Cancel
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
