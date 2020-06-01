import React, { Component } from 'react';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { connect } from 'react-redux';


class CreateTimesheetModal extends Component {
    constructor(props) {
        super(props);
        this.state = {
            projectId: props.projectId,
            date: new Date(),
            hours_count: 0
        };
    }

    changeDateHandler = (event) => {
        this.setState({ date: new Date(event.target.value) })
    }

    changeHoursCountHandler = (event) => {
        this.setState({ hours_count: Number.parseInt(event.target.value) })
    }

    handlerSave = () => {
        if (this.state.date === null ||
            this.state.hours_count === null ||
            this.state.hours_count === '' ||
            this.state.hours_count === 0) {
            alert("Некорректные данные!");
        } else {
            this.props.onSave(this.props.taskId, this.state.date, this.state.hours_count, "");
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
                        Создание отчета по времени
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group controlId="date">
                            <Form.Label>Дата</Form.Label>
                            <Form.Control onChange={this.changeDateHandler} placeholder="Укажите дату" />
                        </Form.Group>

                        <Form.Group controlId="count">
                            <Form.Label>Количество</Form.Label>
                            <Form.Control onChange={this.changeHoursCountHandler} placeholder="Укажите количество затраченных часов" />
                        </Form.Group>

                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button
                        onClick={() => this.handlerSave()}
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
    date: new Date(),
    hours_count: 0
});

export default connect(mapStateToProps, null)(CreateTimesheetModal);