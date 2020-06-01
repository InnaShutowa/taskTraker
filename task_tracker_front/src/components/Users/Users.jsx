import React, { Component } from 'react';
import ListGroup from 'react-bootstrap/ListGroup';
import Button from 'react-bootstrap/Button'
import * as css from "./Styles.css";
import CreateUserModal from '../CreateUserModal';
import { connect } from 'react-redux';
import ActionTypes from '../../store/Actions/index.js';
import axios from 'axios';
import { saveAs } from 'file-saver';
import Form from 'react-bootstrap/Form';

class Users extends Component {

    constructor(props) {
        super(props);
        this.state = {
            users: [],
            isShow: false,
            isRequested: false,
        };
    }

    componentDidMount = () => {
        this.getUsersListHandler();
    };

    createUserHandler = (data) => {
        axios.post('http://localhost:57392/user',
            {
                ...data,
                apikey: this.props.userReducer.apikey
            }).then(response => {
                if (response.data.error) {
                    alert(response.data.error);
                } else {
                    alert("Внимание! Сохраните пароль для авторизации пользователя: " + response.data.data);
                }
                this.setShow(false);
                this.getUsersListHandler();
            });
    }

    getUsersListHandler = () => {
        axios.get('http://localhost:57392/user?apikey=' + this.props.userReducer.apikey).then(response => {
            if (response.data.error) {
                alert(response.data.error);
            } else {
                this.setState({ users: response.data.data, isRequested: true });
            }
        });
    }

    getStatisticHandler = () => {
        axios.get('http://localhost:57392/statistic?apikey=' + this.props.userReducer.apikey).then(response => {
            if (response.data.error) {
                alert(response.data.error);
            } else {
                console.log(response.data);
                let filename = "tasks_statistic.xls";

                let url = window.URL
                    .createObjectURL(new Blob([response.data]));
                saveAs(url, filename);
            }
        });
    }


    setShow = (value) => {
        this.setState({ isShow: value });
    }

    render() {

        if (this.state.isRequested) {
            return <div className="main_fon">
                <div className="inline_block_common">
                    <div className="inline_block">
                        <div className="text_title">Пользователи: </div>
                        <Button className="button_padding" variant="outline-primary" onClick={() => this.getStatisticHandler()}>Выгрузить статистику</Button>
                    </div>
                    <Button className="button_padding_1" variant="outline-primary" onClick={() => this.setShow(true)}>Добавить пользователя</Button>
                   
                </div>


                <ListGroup>
                    <ListGroup.Item className="inline_block">
                        <div className="inline_element" href="/proj">Имя</div>
                        <div className="inline_element_email">email</div>
                        <div className="inline_element">Количество незакрытых задач</div>
                        <div className="inline_element">Количество закрытых задач</div>
                    </ListGroup.Item>
                    {
                        this.state.users.map(usr => (
                            <ListGroup.Item className="inline_block">
                                <a className="inline_element" href={"/userPage/" + usr.user_id}>{usr.full_name}</a>
                                <div className="inline_element_email">{usr.email}</div>
                                <div className="inline_element">{usr.count_open_tasks}</div>
                                <div className="inline_element">{usr.count_closed_tasks}</div>
                            </ListGroup.Item>
                        ))
                    }
                </ListGroup>
                <CreateUserModal
                    show={this.state.isShow}
                    onHide={() => this.setShow(false)}
                    onSave={(data) => this.createUserHandler(data)} />
            </div>;
        } else {
            return <div>Loading...</div>
        }
    }
}

const mapStateToProps = (state) => ({
    userReducer: state.UserReduser,
    user: state.Users,
    isShow: false,
    isRequested: state.Users ? state.Users.IsRequested : false,
});

export default connect(
    mapStateToProps,
    null
)(Users);