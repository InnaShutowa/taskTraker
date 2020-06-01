import React, { Component } from 'react';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Button from 'react-bootstrap/Button';
import * as css from "./TaskStyles.css";
import { connect } from 'react-redux';
import axios from 'axios';
import Moment from 'react-moment';
import DeleteConfirmModal from '../DeleteConfirmModal';
import CreateTimesheetModal from '../CreateTimesheetModal';
import ChangeTaskStatusModal from '../ChangeTaskStatusModal';




class Task extends Component {
    constructor(props) {
        super(props);
        this.state = {
            task: {
            },
            taskId: Number.parseInt(props.location.pathname.substring(props.location.pathname.lastIndexOf('/') + 1)),
            isRequested: false,
            isShow: false,
            isShowTimesheet: false,
            isShowTaskChange: false
        };
    }

    componentDidMount = () => {
        this.getUserInfoHandler();
    };

    setShow = (value) => {
        this.setState({ isShow: value });
    }

    setShowTimesheet = (value) => {
        this.setState({ isShowTimesheet: value });
    }
    
    setShowTaskChange = (value) => {
        this.setState({ isShowTaskChange: value });
    }

    getUserInfoHandler = () => {
        axios.get('http://localhost:57392/singleTask?apikey='+this.props.user.apikey+'&task_id=' + this.state.taskId)
            .then(response => {
                if (response.data.error) {
                    alert(response.data.error);
                } else {
                    this.setState({ task: response.data.data, isRequested: true });
                }
            });
    }

    deleteTaskHandler = (taskId) => {
        axios.post('http://localhost:57392/taskJobs',
            {
                apikey: this.props.user.apikey,
                task_id: taskId
            })
            .then(response => {
                if (response.data.error) {
                    alert(response.data.error);
                } else {
                    this.setShow(false);
                    this.props.history.push(`/tasks`);
                }
            });
    }

    createTimesheetHandler = (taskId, dateCreate, time, apikey) => {
        axios.post('http://localhost:57392/timesheet',
            {
                apikey: this.props.user.apikey,
                task_id: this.state.taskId,
                date_create: dateCreate,
                time: time
            })
            .then(response => {
                if (response.data.error) {
                    alert(response.data.error);
                } else {
                    this.setShow(false);
                    this.props.history.push(`/tasks`);
                }
            });
    }

    changeTaskStatusHandler = (dateCreate, status) => {
        axios.post('http://localhost:57392/singleUser',
        {
            apikey: this.props.user.apikey,
            task_id: this.state.taskId,
            date: dateCreate,
            status: status
        })
        .then(response => {
            if (response.data.error) {
                alert(response.data.error);
            } else {
                this.setShowTaskChange(false);
                this.getUserInfoHandler();
            }
        });
    }

    render() {
        if (this.state.isRequested) {
            return <div className="main">
                <div className="inline_block_top">
                    <div className="text_title_ag">Информация о задаче: </div>
                    {/* <Button onClick={() => this.setShow(true)} className="button_padding" variant="outline-primary">
                        Удалить задачу
                    </Button> */}
                    <Button onClick={() => this.setShowTimesheet(true)} className="button_padding" variant="outline-primary">
                        Добавить отчет
                    </Button>
                </div>
                <Form>
                    <Form.Group as={Row} controlId="formHorizontalEmail">
                        <Form.Label className="boldLineHead" column sm={4}>
                            Название
                    </Form.Label>
                        <Col sm={8}>
                            <Form.Label column sm={12}>
                                {
                                    this.state.task.task_name
                                }
                            </Form.Label>
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} controlId="formHorizontalEmail">
                        <Form.Label className="boldLineHead" column sm={4}>
                            Описание
                    </Form.Label>
                        <Col sm={8}>
                            <Form.Label column sm={12}>
                                {
                                    this.state.task.task_description
                                }
                            </Form.Label>
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} controlId="formHorizontalEmail">
                        <Form.Label className="boldLineHead" column sm={4}>
                            Датa постановки
                    </Form.Label>
                        <Col sm={8}>
                            {
                                this.state.task.date_deadline && <Form.Label column sm={12}>
                                    <Moment format="DD.MM.YYYY">
                                        {
                                            this.state.task.date_create
                                        }
                                    </Moment>
                                </Form.Label>
                            }
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} controlId="formHorizontalEmail">
                        <Form.Label className="boldLineHead" column sm={4}>
                            Дата дедлайна
                    </Form.Label>
                        <Col sm={8}>
                            {
                                this.state.task.date_deadline && <Form.Label column sm={12}>
                                    <Moment format="DD.MM.YYYY">
                                        {
                                            this.state.task.date_deadline
                                        }
                                    </Moment>
                                </Form.Label>
                            }

                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} controlId="formHorizontalEmail">
                        <Form.Label className="boldLineHead" column sm={4}>
                            Дата завершения
                    </Form.Label>
                        <Col sm={8}>
                            {
                                this.state.task.date_finished && <Form.Label column sm={12}>
                                    <Moment format="DD.MM.YYYY">
                                        {
                                            new Date(this.state.task.date_finished)
                                        }
                                    </Moment>
                                </Form.Label>
                            }
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} controlId="formHorizontalEmail">
                        <Form.Label className="boldLineHead" column sm={4}>
                            Исполнитель
                    </Form.Label>
                        <Col sm={8}>
                            <Form.Label column sm={12}>
                                <a href={"/userPage/"+this.state.task.user_id}>
                                    {
                                        this.state.task.user_full_name
                                    }
                                </a>
                            </Form.Label>
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} controlId="formHorizontalEmail">
                        <Form.Label className="boldLineHead" column sm={4}>
                            Создатель задачи
                        </Form.Label>
                        <Col sm={8}>
                            <Form.Label column sm={12}>
                                <a href={"/userPage/"+this.state.task.creater_id}>
                                    {
                                        this.state.task.creater_full_name
                                    }
                                </a>
                            </Form.Label>
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row} controlId="formHorizontalEmail">
                        <Form.Label className="boldLineHead" column sm={4}>
                            Статус
                        </Form.Label>
                        <Col sm={2}>
                            <Form.Label column sm={12}>
                                {
                                    this.state.task.task_status_text
                                }
                            </Form.Label>
                        </Col>
                        <Col sm={6}>
                            <Form.Label column sm={12}>
                                <Button onClick={() => this.setShowTaskChange(true)} className="button_padding" variant="outline-primary">
                                    Изменить статус
                                </Button>
                            </Form.Label>
                        </Col>
                    </Form.Group>
                </Form>

                <DeleteConfirmModal show={this.state.isShow}
                    projectId={this.state.taskId}
                    onHide={() => this.setShow(false)}
                    onSave={(taskId) => this.deleteTaskHandler(taskId)}
                />

                <CreateTimesheetModal show={this.state.isShowTimesheet}
                    taskId={this.state.taskId}
                    onHide={() => this.setShowTimesheet(false)}
                    onSave={(taskId, dateCreate, time, apikey) => this.createTimesheetHandler(taskId, dateCreate, time, apikey)}
                />

                <ChangeTaskStatusModal show={this.state.isShowTaskChange}
                    projectId={this.state.taskId}
                    onHide={() => this.setShowTaskChange(false)}
                    onSave={(dateCreate, status) => this.changeTaskStatusHandler(dateCreate, status)}
                />

            </div>;
        } else {
            return <div>Loading...</div>
        }
    };
}

const mapStateToProps = (state) => ({
    user: state.UserReduser,
    task: state.Task,
    taskId: 0,
    isShow: false,
    isShowTimesheet: false,
    isShowTaskChange: false
});

export default connect(
    mapStateToProps,
    null
)(Task);