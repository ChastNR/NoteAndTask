import React from "react";
import Modal from "react-modal";
import "./AddNewTaskModal.css";
import { request } from "../../libs/api";
import "../css/global.css";

const customStyles = {
  content: {
    top: "50%",
    left: "50%",
    right: "auto",
    bottom: "auto",
    marginRight: "-50%",
    transform: "translate(-50%, -50%)"
  }
};

export class AddNewTaskModal extends React.Component {
  static displayName = AddNewTaskModal.name;

  constructor(props) {
    super(props);

    this.state = {
      modalIsOpen: false,
      lists: null
    };

    this.openModal = this.openModal.bind(this);
    this.closeModal = this.closeModal.bind(this);
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
      var formData;

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
      this.closeModal();
    } else {
      event.target.reportValidity();
    }
  };

  openModal() {
    this.setState({ modalIsOpen: true });
  }

  closeModal() {
    this.setState({ modalIsOpen: false });
  }

  render() {
    return (
      <div>
        <button
          type="button"
          className="link-button"
          onClick={() => {
            this.openModal();
            this.loadLists();
          }}
        >
          Add new task
        </button>

        <Modal
          className="modal-shadow"
          isOpen={this.state.modalIsOpen}
          onRequestClose={this.closeModal}
          ariaHideApp={false}
          style={customStyles}
        >
          <div className="modal-header">
            <h5 className="modal-title">Add new task</h5>
            <button type="button" className="close" onClick={this.closeModal}>
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <form onSubmit={this.handleSubmit}>
            <div className="modal-body">
              <div className="form-group">
                <label className="control-label">Name:</label>
                <input
                  type="text"
                  name="name"
                  className="form-control"
                  required
                />
              </div>
              <div className="form-group">
                <label className="control-label">Description:</label>
                <textarea
                  name="description"
                  className="form-control"
                  rows="4"
                  cols="50"
                  required
                />
              </div>
              <div className="form-group">
                <label className="control-label">Expires on?</label>
                <input
                  name="expiresOn"
                  className="form-control"
                  type="datetime-local"
                  required
                />
              </div>
              <div className="form-group">
                <label className="control-label">Choose Task List:</label>
                {this.state.lists && (
                  <select name="listId" className="form-control">
                    <option />
                    {this.state.lists.map(list => (
                      <option value={list.id}>{list.name}</option>
                    ))}
                  </select>
                )}
              </div>
            </div>
            <div className="modal-footer">
              <button
                type="button"
                className="btn btn-outline-danger"
                onClick={this.closeModal}
              >
                Close
              </button>
              <button type="submit" className="btn btn-outline-primary">
                Add
              </button>
            </div>
          </form>
        </Modal>
      </div>
    );
  }
}
