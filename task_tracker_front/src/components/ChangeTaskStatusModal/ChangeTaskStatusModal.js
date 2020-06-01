import React, { Component } from 'react';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { connect } from 'react-redux';
import { DropdownButton, Dropdown, InputGroup } from 'react-bootstrap';


class ChangeTaskStatusModal extends Component {
    constructor(props) {
        super(props);
        this.state = {
            taskId: props.taskId,
            taskStatus: 0,
            date: null
        };
    }

    changeDateHandler = (event) => {
        this.setState({ date: new Date(event.target.value) })
    }

    selectHandler = (event) => {
        let number = 0;
        if (event.target.innerHTML === "New") {
            number = 0;
        } else if (event.target.innerHTML === "Develop") {
            number = 1;
        } else if (event.target.innerHTML === "QA") {
            number = 2;
        } else if (event.target.innerHTML === "Ready") {
            number = 3;
        }

        this.setState({ taskStatus: number });
    }

    saveHandler = () => {
        if (this.state.taskStatus === 3 && this.state.date === null) {
            alert("Заполните данные!");
        } else {
            this.props.onSave(this.state.date, this.state.taskStatus);
        }
    }

    render() {
        return (
            <Modal
                show={this.props.show}
                onHide={this.props.onHide}
                size="lg"
                aria-labelledby="contained-modal-title-vcenter"
                centered
            >
                <Modal.Header closeButton>
                    <Modal.Title id="contained-modal-title-vcenter">
                        Изменить статус задачи
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group controlId="date">
                            <Form.Label>Дата</Form.Label>
                            <Form.Control onChange={this.changeDateHandler} placeholder="Укажите дату" />
                        </Form.Group>

                        <Form.Group controlId="date">
                            <Form.Label>Статус</Form.Label>
                            <DropdownButton
                                as={InputGroup.Prepend}
                                variant="outline-secondary"
                                title="Статус"
                                id="input-group-dropdown-1"
                            >
                                <Dropdown.Item onClick={this.selectHandler} href="#">New</Dropdown.Item>
                                <Dropdown.Item onClick={this.selectHandler} href="#">Develop</Dropdown.Item>
                                <Dropdown.Item onClick={this.selectHandler} href="#">QA</Dropdown.Item>
                                <Dropdown.Item onClick={this.selectHandler} href="#">Ready</Dropdown.Item>
                            </DropdownButton>
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button
                        onClick={() => this.saveHandler()}
                        variant="primary"
                        type="submit">
                        Выполнить
                    </Button>
                    <Button onClick={this.props.onHide}>Закрыть</Button>
                </Modal.Footer>
            </Modal>
        );
    };
}

const mapStateToProps = (state) => ({
    taskStatus: state.taskStatus,
    date: null
});

export default connect(mapStateToProps, null)(ChangeTaskStatusModal);