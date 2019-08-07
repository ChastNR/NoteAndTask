import Modal from "react-bootstrap/Modal";
import ButtonToolbar from "react-bootstrap/ButtonToolbar";
import Button from "react-bootstrap/Button";
import {BoardLayout} from "./BoardLayout";
import * as React from "react";

async function handleSubmit(event) {
    event.preventDefault();

    if (event.target.checkValidity()) {

        await fetch("api/task/addNewList?name=" + event.target.name.value , {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                'Authorization': 'Bearer ' + localStorage.getItem("token")
            }
        });
        // window.location.href = "/board?lists=" + true;
    } else {
        event.target.reportValidity()
    }
}

function MyVerticallyCenteredModal(props) {
    return (
        <Modal
            {...props}
            size="sm"
            aria-labelledby="contained-modal-title-vcenter"
            centered
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Add new task list
                </Modal.Title>
            </Modal.Header>
            <form onSubmit={handleSubmit}>
                <Modal.Body>
                    <div className="form-group">
                        <label className="control-label">List name:</label>
                        <input name="name" className="form-control"/>
                    </div>
                </Modal.Body>
                <Modal.Footer>
                    <input type="submit" value="Add" className="btn btn-primary"/>
                    <Button onClick={props.onHide}>Close</Button>
                </Modal.Footer>
            </form>
        </Modal>
    );
}


export function AddNewList() {
    const [modalShow, setModalShow] = React.useState(false);

    return (
        <ButtonToolbar>
            <button onClick={() => setModalShow(true)} className="btn btn-link"><i className="far fa-plus-square mr-3"/>Add new list
            </button>

            <MyVerticallyCenteredModal
                show={modalShow}
                onHide={() => setModalShow(false)}
            />
        </ButtonToolbar>
    );
}