import React, { Component } from 'react';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { connect } from 'react-redux';

import * as css from "./Styles.css";
import ActionTypes from '../../store/Actions/index.js';
import { Redirect } from 'react-router-dom';


class CreateProjectModal extends Component {
    constructor(props) {
        super(props);
        this.state = {
            name: "",
            description: ""
        };
    }

    handlerName = (event) => {
        this.setState({name: event.target.value})
    }

    handlerDescr = (event) => {
        this.setState({description: event.target.value});
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
                        Создание проекта
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        
                        <Form.Group controlId="name">
                            <Form.Label>
                                <div className="inline_elements">
                                    <div className="elements">Название</div>
                                    <div style={{color: "red"}} className="elements">*</div>
                                </div>

                            </Form.Label>
                            <Form.Control onChange={this.handlerName} placeholder="Название проекта" />
                        </Form.Group>

                        <Form.Group controlId="description">
                            <Form.Label>Описание</Form.Label>
                            <Form.Control  onChange={this.handlerDescr} placeholder="Описание" />
                        </Form.Group>

                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button 
                    onClick={()=>this.props.onSave(this.state.name, this.state.description)} 
                    variant="primary" 
                    type="submit">
                        Сохранить
                    </Button>
                    <Button onClick={this.props.onHide}>Закрыть</Button>
                </Modal.Footer>
            </Modal>
        );
    };
}



const mapStateToProps = (state) => ({
    name: state.name,
    description: state.description
});

export default connect (mapStateToProps, null) (CreateProjectModal);