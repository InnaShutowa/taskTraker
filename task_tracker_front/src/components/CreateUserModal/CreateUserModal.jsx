import React, { Component }  from 'react';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import * as css from "./Styles.css";
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { connect } from 'react-redux';
import axios from 'axios';

class CreateUserModal extends Component {

    constructor(props) {
        super(props);
        this.state = {
            first_name: "",
            last_name: "",
            phone: "",
            email: "",
            birth_date: new Date(),
            is_admin: false
        };
    }

    handlerFirstName = (event) => {
        this.setState({first_name: event.target.value})
    }

    handlerLastName = (event) => {
        this.setState({last_name: event.target.value})
    }

    handlerPhone = (event) => {
        this.setState({phone: event.target.value})
    }

    handlerEmail = (event) => {
        this.setState({email: event.target.value})
    }

    handlerBirthDate = (event) => {

        this.setState({birth_date: new Date(event.target.value)})
    }

    handlerIsAdmin = (event) => {
        this.setState({is_admin: !this.state.is_admin})
    }

    handlerSave = () => {

        if (this.state.first_name === null ||
            this.state.first_name === '' ||
            this.state.last_name === null ||
            this.state.last_name === '' ||
            this.state.phone === null ||
            this.state.phone === '' ||
            (this.state.phone.length !== 11 &&
            this.state.phone.length !== 12) ||
            this.state.email === null ||
            this.state.email === '' ) {
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
                        Регистрация пользователя
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Row>
                            <Col>
                                <Form.Label>Имя</Form.Label>
                                <Form.Control onChange={this.handlerFirstName} placeholder="Имя" />
                            </Col>
                            <Col>
                                <Form.Label>Фамилия</Form.Label>
                                <Form.Control onChange={this.handlerLastName} placeholder="Фамилия" />
                            </Col>
                        </Row>
                        <Form.Group controlId="email">
                            <Form.Label>Email</Form.Label>
                            <Form.Control onChange={this.handlerEmail} placeholder="example@example.ru" />
                        </Form.Group>
    
                        <Form.Group controlId="phone">
                            <Form.Label>Телефон</Form.Label>
                            <Form.Control onChange={this.handlerPhone} placeholder="+8 888 888 88 88" />
                        </Form.Group>
    
                        <Form.Group controlId="birthday">
                            <Form.Label>Дата рождения</Form.Label>
                            <Form.Control onChange={this.handlerBirthDate} placeholder="09.19.1998" />
                        </Form.Group>
    
                        <Form.Group controlId="is_admin">
                            <Form.Check  
                                onChange={this.handlerIsAdmin}  
                                type="checkbox" 
                                label="Включить права администратора" />
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
    first_name: state.first_name,
    last_name: state.last_name,
    phone: state.phone,
    email: state.email,
    birth_date: state.birth_date,
    is_admin: state.is_admin
});

export default connect (mapStateToProps, null) (CreateUserModal);