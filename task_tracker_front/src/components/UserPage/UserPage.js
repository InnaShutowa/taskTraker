import React, { Component } from 'react';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Button from 'react-bootstrap/Button';
import { connect } from 'react-redux';
import axios from 'axios';
import Moment from 'react-moment';
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
import DeleteConfirmModal from '../DeleteConfirmModal';
import * as css from "./StylesUserPage.css";
import { saveAs } from 'file-saver';
import * as jsPDF from 'jspdf';



class UserPage extends Component {
    constructor(props) {
        super(props);
        this.state = {
            user: {
                first_name: '',
                last_name: '',
                is_admin: '',
                birth_date: new Date()
            },
            isRequested: false,
            isShow: false,
            isShowGetForm: false,
            start: null,
            finish: null,
            userId: Number.parseInt(props.location.pathname.substring(props.location.pathname.lastIndexOf('/') + 1))
        };
    }

    componentDidMount = () => {
        this.getUserInfoHandler();
    };

    getUserInfoHandler = () => {
        axios.get('http://localhost:57392/admin?apikey=' + this.props.userReducer.apikey + '&user_id=' + this.state.userId)
            .then(response => {
                if (response.data.error) {
                    alert(response.data.error);
                } else {
                    this.setState({ user: response.data.data, isRequested: true });
                }
            });
    }

    setShow = (value) => {
        this.setState({ isShow: value });
    }

    deleteUserHandler = (userId) => {
        axios.post('http://localhost:57392/userPage', {
            apikey: this.props.userReducer.apikey,
            user_id: userId
        })
            .then(response => {
                if (response.data.error) {
                    alert(response.data.error);
                } else {
                    this.props.history.push(`/users`);
                }
            });
    }

    CellTaskIdFormatter = (cell, row) => {
        return (<div><a href={"/task/" + cell}>{cell}</a></div>);
    }

    CellFormatter = (cell, row) => {
        return (<div>
            <Moment format="DD.MM.YYYY">
                {
                    new Date(cell)
                }
            </Moment>
        </div>);
    }

    setStart = (value) => {
        this.setState({ start: value.target.value });
    }

    setFinish = (value) => {
        this.setState({ finish: value.target.value });
    }

    setShowGetForm = () => {
        this.setState({ isShowGetForm: true });
    }


    getFormHandler = () => {
        axios.post('http://localhost:57392/statistic',
            {
                apikey: this.props.userReducer.apikey,
                user_id: this.state.userId,
                start: this.state.start,
                finish: this.state.finish
            }).then(response => {
                console.log(response);
                if (response.data.error) {
                    alert(response.data.error);
                } else {
                    let filename = "tasks_form.pdf";

                    var pdf = new jsPDF();
                    pdf.text(10, 10, response.data);
                    pdf.setFont("times");
                    pdf.setFontType("bold");
                    pdf.setFontSize(9);
                    pdf.save(filename);
                }
            });
    }



    render() {
        if (this.state.isRequested) {
            return <div className="main_fon">
                <div className="inline">
                    <div className="headingPage">
                        Информация о пользователе
                    </div>
                       {
                            this.props.userReducer.is_admin  
                            && <Button className="lineValueButton" onClick={() => this.setShow(true)} variant="primary" type="submit">
                                    Удалить
                            </Button>
                       }
                    

                    {
                        this.state.isShowGetForm && <div className="inline_block">
                            <Form>
                                <Form.Group controlId="birthday">
                                    <Form.Label>Начало периода</Form.Label>
                                    <Form.Control type="date" onChange={this.setStart} placeholder="01.19.1998" />
                                </Form.Group>

                                <Form.Group controlId="birthday">
                                    <Form.Label>Конец периода</Form.Label>
                                    <Form.Control type="date" onChange={this.setFinish} placeholder="01.19.1998" />
                                </Form.Group>

                            </Form>
                            <Button
                                className="button_padding"
                                variant="outline-primary"
                                onClick={this.getFormHandler}>
                                Получить форму
                            </Button>
                        </div>
                    }

                    {
                        this.props.userReducer.is_admin 
                        && !this.state.isShowGetForm 
                        && this.state.user 
                        && this.state.user.tasks_list 
                        && this.state.user.tasks_list.length !== 0 &&
                        <Button
                            className="button_padding"
                            variant="outline-primary"
                            onClick={() => this.setShowGetForm()}>
                            Получить форму
                            </Button>
                    }
                </div>
                <div className="form_class">
                    <Form>
                        <Form.Group as={Row} controlId="formHorizontalEmail">
                            <Form.Label className="boldLineHead" column sm={2}>
                                Имя
                        </Form.Label>
                            <Col sm={10}>
                                <Form.Label column sm={2}>
                                    {
                                        this.state.user.first_name
                                    }
                                </Form.Label>
                            </Col>
                        </Form.Group>
                        <Form.Group as={Row} controlId="formHorizontalEmail">
                            <Form.Label className="boldLineHead" column sm={2}>
                                Фамилия
                        </Form.Label>
                            <Col sm={10}>
                                <Form.Label column sm={2}>
                                    {
                                        this.state.user.last_name
                                    }
                                </Form.Label>
                            </Col>
                        </Form.Group>
                        <Form.Group as={Row} controlId="formHorizontalEmail">
                            <Form.Label className="boldLineHead" column sm={2}>
                                Номер телефона
                        </Form.Label>
                            <Col sm={10}>
                                <Form.Label column sm={2}>
                                    {
                                        this.state.user.phone
                                    }
                                </Form.Label>
                            </Col>
                        </Form.Group>
                        <Form.Group as={Row} controlId="formHorizontalEmail">
                            <Form.Label className="boldLineHead" column sm={2}>
                                Email
                        </Form.Label>
                            <Col sm={10}>
                                <Form.Label column sm={2}>
                                    {
                                        this.state.user.email
                                    }
                                </Form.Label>
                            </Col>
                        </Form.Group>
                        <Form.Group as={Row} controlId="formHorizontalEmail">
                            <Form.Label className="boldLineHead" column sm={2}>
                                Дата рождения
                        </Form.Label>
                            <Col sm={10}>
                                <Form.Label column sm={2}>
                                    <Moment format="DD.MM.YYYY">
                                        {
                                            new Date(this.state.user.birth_date)
                                        }
                                    </Moment>
                                </Form.Label>
                            </Col>
                        </Form.Group>
                        <Form.Group as={Row} controlId="formHorizontalEmail">
                            <Form.Label className="boldLineHead" column sm={2}>
                                Роль
                            </Form.Label>
                            <Col sm={10}>
                                <Form.Label column sm={2}>
                                    {
                                        this.state.user.is_admin ? "ADMIN" : "USER"
                                    }
                                </Form.Label>
                            </Col>
                        </Form.Group>
                    </Form>
                </div>

                {this.props.userReducer.is_admin 
                && <div>
                    <div className="inline_block_users_title">
                        Задачи пользователя
                    </div>
                    <div className="text_description">
                        <BootstrapTable data={this.state.user.tasks_list} striped hover>
                            <TableHeaderColumn isKey dataField='task_id' dataFormat={this.CellTaskIdFormatter}>Id задачи</TableHeaderColumn>
                            <TableHeaderColumn dataField='task_name'>Название задачи</TableHeaderColumn>
                            <TableHeaderColumn dataField='date_create' dataFormat={this.CellFormatter}>Дата постановки</TableHeaderColumn>
                            <TableHeaderColumn dataField='hours_count'>Затраченное время</TableHeaderColumn>
                            <TableHeaderColumn dataField='task_status_text'>Статус</TableHeaderColumn>
                        </BootstrapTable>
                    </div>

                    <div className="inline_block_users_title">
                        Отчеты пользователя
                    </div>
                    <div className="text_description">
                        <BootstrapTable data={this.state.user.tasks_list} striped hover>
                            <TableHeaderColumn isKey dataField='task_id' dataFormat={this.CellTaskIdFormatter}>Название задачи</TableHeaderColumn>
                            <TableHeaderColumn dataField='task_name'>Название проекта</TableHeaderColumn>
                            <TableHeaderColumn dataField='date_create' dataFormat={this.CellFormatter}>Дата создания</TableHeaderColumn>
                            <TableHeaderColumn dataField='hours_count'>Количество часов</TableHeaderColumn>
                        </BootstrapTable>
                    </div>
                </div>}
                <DeleteConfirmModal show={this.state.isShow}
                    projectId={this.state.userId}
                    onHide={() => this.setShow(false)}
                    onSave={(userId) => this.deleteUserHandler(userId)} />

           
                </div>
        } else {
            return <div>Loading...</div>
        }
    };
}

const mapStateToProps = (state) => ({
    userReducer: state.UserReduser,
    user: state.User,
    userId: 0,
    isShow: false,
    start: null,
    finish: null,
    isShowGetForm: false,
});

export default connect(
    mapStateToProps,
    null
)(UserPage);