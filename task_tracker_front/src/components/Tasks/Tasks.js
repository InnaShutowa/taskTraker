import React, { Component } from 'react';
import ListGroup from 'react-bootstrap/ListGroup';
import Button from 'react-bootstrap/Button';
import CreateProjectModal from '../CreateProjectModal';
import { connect } from 'react-redux';
import ActionTypes from '../../store/Actions/index.js';
import axios from 'axios';
import * as css from "./StylesTasks.css";
import Moment from 'react-moment';
import Form from 'react-bootstrap/Form';
import * as jsPDF from 'jspdf';


class Tasks extends Component {
    constructor(props) {
        super(props);
        this.state = {
            tasks: {
                admin_task_list: [],
                new_tasks: [],
                dev_tasks: [],
                qa_tasks: [],
                ready_tasks: []
            },
            isShow: false,
            isRequested: false
        };
    }

    componentDidMount = () => {
        this.getProjectsListHandler();
    };

    getProjectsListHandler = () => {
        axios.get('http://localhost:57392/task?apikey='+this.props.user.apikey).then(response => {
            if (response.data.error) {
                alert(response.data.error);
            } else {
                this.setState({ tasks: response.data.data, isRequested: true });
            }
        });
    }

    createNewProjectHandler = (name, descr) => {
        axios.post('http://localhost:57392/project',
            {
                apikey: this.props.user.apikey,
                project_name: name,
                description: descr
            }).then(response => {
                if (response.data.error) {
                    alert(response.data.error);
                } else {
                    this.setShow(false);
                    this.getProjectsListHandler();
                }
            });
    }

    setShow = (value) => {
        this.setState({ isShow: value });
    }

    getFormHandler = () => {
        axios.post('http://localhost:57392/statistic',
            {
                apikey: this.props.user.apikey,
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
        console.log(this.state);
        if (this.state.isRequested) {
            return <div className="main_fon">
                {
                        <div className="inline_block">
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


                <div className="inline_block_top">
                    <div className="text_title_ag">Новые задачи: </div>
                </div>
                <ListGroup>
                    <ListGroup.Item className="inline_block">
                        <div className="inline_element">Название</div>
                        <div className="inline_element">Дата создания</div>
                        <div className="inline_element">Количество часов</div>
                        <div className="inline_element">Создатель</div>
                        <div className="inline_element">Статус</div>
                    </ListGroup.Item>
                    {
                        this.state.tasks.new_tasks.map(task => (
                            <ListGroup.Item className="inline_block">
                                <a className="inline_element" href={"/task/" + task.task_id}>{task.task_name}</a>
                                <div className="inline_element">
                                    <Moment format="DD.MM.YYYY">
                                        {
                                            new Date(task.date_create)
                                        }
                                    </Moment>
                                </div>
                                <div className="inline_element">{task.hours_count}</div>
                                <div className="inline_element">{task.creater_full_name}</div>
                                <div className="inline_element">{task.task_status_text}</div>
                            </ListGroup.Item>
                        ))
                    }
                </ListGroup>

                <div className="inline_block_top">
                    <div className="text_title_ag">Development: </div>
                </div>
                <ListGroup>
                    <ListGroup.Item className="inline_block">
                        <div className="inline_element">Название</div>
                        <div className="inline_element">Дата создания</div>
                        <div className="inline_element">Количество часов</div>
                        <div className="inline_element">Создатель</div>
                        <div className="inline_element">Статус</div>
                    </ListGroup.Item>
                    {
                        this.state.tasks.dev_tasks.map(task => (
                            <ListGroup.Item className="inline_block">
                                <a className="inline_element" href={"/task/" + task.task_id}>{task.task_name}</a>
                                <div className="inline_element">
                                    <Moment format="DD.MM.YYYY">
                                        {
                                            new Date(task.date_create)
                                        }
                                    </Moment>
                                </div>
                                <div className="inline_element">{task.hours_count}</div>
                                <div className="inline_element">{task.creater_full_name}</div>
                                <div className="inline_element">{task.task_status_text}</div>
                            </ListGroup.Item>
                        ))
                    }
                </ListGroup>

                <div className="inline_block_top">
                    <div className="text_title_ag">QA: </div>
                </div>
                <ListGroup>
                    <ListGroup.Item className="inline_block">
                        <div className="inline_element">Название</div>
                        <div className="inline_element">Дата создания</div>
                        <div className="inline_element">Количество часов</div>
                        <div className="inline_element">Создатель</div>
                        <div className="inline_element">Статус</div>
                    </ListGroup.Item>
                    {
                        this.state.tasks.qa_tasks.map(task => (
                            <ListGroup.Item className="inline_block">
                                <a className="inline_element" href={"/task/" + task.task_id}>{task.task_name}</a>
                                <div className="inline_element">
                                    <Moment format="DD.MM.YYYY">
                                        {
                                            new Date(task.date_create)
                                        }
                                    </Moment>
                                </div>
                                <div className="inline_element">{task.hours_count}</div>
                                <div className="inline_element">{task.creater_full_name}</div>
                                <div className="inline_element">{task.task_status_text}</div>
                            </ListGroup.Item>
                        ))
                    }
                </ListGroup>

                <div className="inline_block_top">
                    <div className="text_title_ag">Завершенные задачи: </div>
                </div>
                <ListGroup>
                    <ListGroup.Item className="inline_block">
                        <div className="inline_element">Название</div>
                        <div className="inline_element">Дата создания</div>
                        <div className="inline_element">Количество часов</div>
                        <div className="inline_element">Создатель</div>
                        <div className="inline_element">Статус</div>
                    </ListGroup.Item>
                    {
                        this.state.tasks.ready_tasks.map(task => (
                            <ListGroup.Item className="inline_block">
                                <a className="inline_element" href={"/task/" + task.task_id}>{task.task_name}</a>
                                <div className="inline_element">
                                    <Moment format="DD.MM.YYYY">
                                        {
                                            new Date(task.date_create)
                                        }
                                    </Moment>
                                </div>
                                <div className="inline_element">{task.hours_count}</div>
                                <div className="inline_element">{task.creater_full_name}</div>
                                <div className="inline_element">{task.task_status_text}</div>
                            </ListGroup.Item>
                        ))
                    }
                </ListGroup>
                {
                        this.props.user.is_admin &&
                        <div>
<div className="inline_block_top">
                        <div className="text_title_ag">Созданные Вами задачи: </div>
                    </div>
                    <ListGroup>
                        <ListGroup.Item className="inline_block">
                            <div className="inline_element">Название</div>
                            <div className="inline_element">Дата создания</div>
                            <div className="inline_element">Количество часов</div>
                            <div className="inline_element">Создатель</div>
                            <div className="inline_element">Статус</div>
                        </ListGroup.Item>
                        {
                            this.state.tasks.admin_task_list.map(task => (
                                <ListGroup.Item className="inline_block">
                                    <a className="inline_element" href={"/task/" + task.task_id}>{task.task_name}</a>
                                    <div className="inline_element">
                                        <Moment format="DD.MM.YYYY">
                                            {
                                                new Date(task.date_create)
                                            }
                                        </Moment>
                                    </div>
                                    <div className="inline_element">{task.hours_count}</div>
                                    <a className="inline_element" href={"/userPage/" + task.creater_id}>{task.creater_full_name}</a>
                                    <div className="inline_element">{task.task_status_text}</div>
                                </ListGroup.Item>
                            ))
                        }
                    </ListGroup>
                        </div>
                    
                }

                <CreateProjectModal show={this.state.isShow}
                    onHide={() => this.setShow(false)}
                    onSave={(name, descr) => this.createNewProjectHandler(name, descr)} />
            </div>;
        } else {
            return <div>Loading...</div>
        }

    };
}


const mapStateToProps = (state) => ({
    user: state.UserReduser,
    tasks: state.Tasks,
    isShow: false,
    isRequested: state.Tasks ? state.Tasks.IsRequested : false
});

export default connect(
    mapStateToProps,
    null
)(Tasks);