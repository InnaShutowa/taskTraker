import React from 'react';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import * as css from "./Styles.css";
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

const CreateUserModal = (props) => {
    return (
        <Modal
            {...props}
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
                            <Form.Control placeholder="Имя" />
                        </Col>
                        <Col>
                            <Form.Label>Фамилия</Form.Label>
                            <Form.Control placeholder="Фамилия" />
                        </Col>
                    </Row>
                    <Form.Group controlId="email">
                        <Form.Label>Email</Form.Label>
                        <Form.Control placeholder="example@example.ru" />
                    </Form.Group>

                    <Form.Group controlId="phone">
                        <Form.Label>Телефон</Form.Label>
                        <Form.Control placeholder="+8 888 888 88 88" />
                    </Form.Group>

                    <Form.Group controlId="birthday">
                        <Form.Label>Дата рождения</Form.Label>
                        <Form.Control placeholder="Др" />
                    </Form.Group>

                    <Row>
                        <Col>
                            <Form.Control placeholder="Введите пароль" />
                        </Col>
                        <Col>
                            <Form.Control placeholder="Повторите пароль" />
                        </Col>
                    </Row>

                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button onClick={props.onHide} variant="primary" type="submit">
                    Сохранить
                </Button>
                <Button onClick={props.onHide}>Закрыть</Button>
            </Modal.Footer>
        </Modal>
    );
}

export default CreateUserModal;