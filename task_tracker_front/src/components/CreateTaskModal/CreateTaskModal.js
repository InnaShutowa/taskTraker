import React, { Component }  from 'react';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import * as css from "./CreateTaskStyles.css";
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { connect } from 'react-redux';
import axios from 'axios';

class CreateTaskModal extends Component {

    constructor(props) {
        super(props);
        this.state = {
            title: "",
            description: "",
            email: "",
            deadline_date: new Date()
        };
    }

    handlerTitle = (event) => {
        this.setState({title: event.target.value})
    }

    handlerDescription = (event) => {
        this.setState({description: event.target.value})
    }

    handlerEmail = (event) => {
        this.setState({email: event.target.value})
    }

    handlerDeadlineDate = (event) => {

        this.setState({deadline_date: new Date(event.target.value)})
    }

    handlerSave = () => {
        if (this.state.title === null ||
            this.state.title === '' ||
            this.state.description === null ||
            this.state.description === '' ||
            this.state.email === null ||
            this.state.email === '' ||
            this.state.deadline_date === null) {
                alert("Некорректные данные!");
            } else {
                this.props.onSave(this.state);
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
                        Создание задачи
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group controlId="email">
                            <Form.Label>Название</Form.Label>
                            <Form.Control onChange={this.handlerTitle} placeholder="Название задачи"/>
                        </Form.Group>
                        
                        <Form.Group controlId="email">
                            <Form.Label>Описание задачи</Form.Label>
                            <Form.Control onChange={this.handlerDescription} placeholder="Описание задачи"/>
                        </Form.Group>

                        <Form.Group controlId="email">
                            <Form.Label>Email</Form.Label>
                            <Form.Control onChange={this.handlerEmail} placeholder="example@example.ru" />
                        </Form.Group>
    
                        <Form.Group controlId="birthday">
                            <Form.Label>Дедлайн</Form.Label>
                            <Form.Control onChange={this.handlerDeadlineDate} placeholder="01.19.1998" />
                        </Form.Group>
    
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button onClick={this.handlerSave} variant="primary" type="submit">
                        Сохранить
                    </Button>
                    <Button onClick={this.props.onHide}>Закрыть</Button>
                </Modal.Footer>
            </Modal>
        );
    };
}


const mapStateToProps = (state) => ({
    title: state.first_name,
    description: state.last_name,
    email: state.email,
    deadline_date: state.birth_date
});

export default connect (mapStateToProps, null) (CreateTaskModal);