import React, { Component } from 'react';
import * as css from "./Styles.css";
import { connect } from 'react-redux';
import ActionTypes from '../../store/Actions';
import axios from 'axios';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';

class Login extends Component {

    constructor(props) {
        super(props);
        this.state = {
            login: "",
            password: ""
        };
    }

    handlerEmail = (event) => {
        this.setState({ login: event.target.value })
    }

    handlerPassword = (event) => {
        this.setState({ password: event.target.value })
    }

    createUserHandler = () => {
        if (this.state.login === null ||
            this.state.login === "" ||
            this.state.password === null ||
            this.state.password === "") {
            alert("Некорректные данные!");
        } else {
            axios.post('http://localhost:57392/authUser',
                {
                    login: this.state.login,
                    password: this.state.password
                }).then(response => {
                    console.log(response);
                    if (response.data.error) {
                        alert(response.data.error);
                    } else {

                        this.props.saveDataAction(response.data.data);
                        window.location.pathname = "/projects";
                    }
                });
        }
    }


    render() {
        return <div className="form_size"><Form >
            <Form.Label className="form_font_characters">Авторизация</Form.Label>
            <Form.Group controlId="email">
                <Form.Label>Email</Form.Label>
                <Form.Control onChange={this.handlerEmail} placeholder="Email" />
            </Form.Group>

            <Form.Group controlId="email">
                <Form.Label>Пароль</Form.Label>
                <Form.Control type="password" onChange={this.handlerPassword} placeholder="Пароль" />
            </Form.Group>

        </Form>
            <Button onClick={this.createUserHandler} variant="primary" type="submit">
                Войти
            </Button>
        </div>
    }
}
const mapDispatchToProps = (dispatch) => {
    return {
        saveDataAction: (data) => {
            dispatch(ActionTypes.ActionTypes.userActions.usersActions.userSaveData(data));
        },
    }
};

const mapStateToProps = (state) => ({
    UserReduser: state.UserReduser,
    login: state.login,
    password: state.password
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)
    (Login);