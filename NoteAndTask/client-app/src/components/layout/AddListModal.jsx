import React from "react";
import Modal from "react-modal";
import "./AddListModal.css";
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

export class AddListModal extends React.Component {
  static displayName = AddListModal.name;

  constructor(props) {
    super(props);

    this.state = {
      modalIsOpen: false,
      lists: null
    };

    this.openModal = this.openModal.bind(this);
    this.closeModal = this.closeModal.bind(this);
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
      }).then(this.closeModal());
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
        <div className="addButton">
          <button
            type="button"
            className="link-button"
            onClick={() => this.openModal()}
          >
            Add new list
          </button>
        </div>
        <Modal
          className="modal-shadow"
          isOpen={this.state.modalIsOpen}
          onRequestClose={this.closeModal}
          ariaHideApp={false}
          style={customStyles}
        >
          <div className="modal-header">
            <h5 className="modal-title">Add new list</h5>
            <button type="button" className="close" onClick={this.closeModal}>
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <form onSubmit={this.handleSubmit}>
            <div className="modal-body">
              <div className="form-group">
                <label className="control-label">Name:</label>
                <input type="text" name="name" className="form-control" />
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
