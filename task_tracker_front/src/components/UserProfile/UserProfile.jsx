import React from 'react';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Button from 'react-bootstrap/Button';
import * as css from "./Styles.css";


const UserProfile = () => {
    return <div className="main">
        <div className="heading">
            Личный кабинет
        </div>
        <Form>
            <Form.Group as={Row} controlId="formHorizontalEmail">
                <Col>
                    <Form.Label column sm={2}>
                        Имя
                    </Form.Label>
                    <Col sm={10}>
                        <Form.Control type="text" placeholder="Имя" />
                    </Col>
                </Col>

                <Col>
                    <Form.Label column sm={2}>
                        Фамилия
                    </Form.Label>
                    <Col sm={10}>
                        <Form.Control type="text" placeholder="Фамилия" />
                    </Col>
                </Col>
            </Form.Group>
            <Form.Group as={Row} controlId="formHorizontalEmail">
                <Form.Label column sm={2}>
                    Номер телефона
                </Form.Label>
                <Col sm={10}>
                    <Form.Control type="text" placeholder="Номер телефона" />
                </Col>
            </Form.Group>
            <Form.Group as={Row} controlId="formHorizontalEmail">
                <Form.Label column sm={2}>
                    Email
                </Form.Label>
                <Col sm={10}>
                    <Form.Control type="email" placeholder="Email" />
                </Col>
            </Form.Group>
            <Form.Group as={Row} controlId="formHorizontalEmail">
                <Form.Label column sm={2}>
                    Дата рождения
                </Form.Label>
                <Col sm={10}>
                    <Form.Control type="email" placeholder="Дата рождения" />
                </Col>
            </Form.Group>

            <Form.Group as={Row}>
                <Col sm={{ span: 10, offset: 2 }}>
                    <Button type="submit">Сохранить</Button>
                </Col>
            </Form.Group>
        </Form>
    </div>;
}

export default UserProfile;