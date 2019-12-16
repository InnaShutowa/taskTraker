import React from 'react';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import * as css from "./Styles.css";
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

const CreateProjectModal = (props) => {
    return (
        <Modal
            {...props}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Создание проекта
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Group controlId="name">
                        <Form.Label>Название</Form.Label>
                        <Form.Control placeholder="Название проекта" />
                    </Form.Group>

                    <Form.Group controlId="description">
                        <Form.Label>Описание</Form.Label>
                        <Form.Control placeholder="Описание" />
                    </Form.Group>

                    <Form.Group controlId="birthday">
                        <Form.Label>Дата рождения</Form.Label>
                        <Form.Control placeholder="Др" />
                    </Form.Group>

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

export default CreateProjectModal;