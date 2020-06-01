import React, { useState, Component } from 'react';
import ListGroup from 'react-bootstrap/ListGroup';
import Button from 'react-bootstrap/Button';
import DeleteConfirmModal from '../DeleteConfirmModal';
import AddUserInProjectModal from '../AddUserInProjectModal';
import CreateTaskModal from '../CreateTaskModal';
import * as css from "./StylesProject.css";
import { BootstrapTable, TableHeaderColumn } from 'react-bootstrap-table';
import projectsActions from '../../store/Actions';
import axios from 'axios';
import { connect } from 'react-redux';
import '../../../node_modules/react-bootstrap-table/dist/react-bootstrap-table-all.min.css';
import { replaceState, withRouter, pushState } from 'react-router';
import { BrowserRouter, Route } from 'react-router-dom'

class Project extends Component {
    constructor(props) {
        super(props);
        this.state = {
            usersToAdd: [],
            project: {},
            isShowAgain: false,
            isShow: false,
            isRequested: false,
            isShowAddUser: false,
            projectId: Number.parseInt(props.location.pathname.substring(props.location.pathname.lastIndexOf('/') + 1))
        };
    }

    componentDidMount = () => {
        this.getProjectHandler("", this.state.projectId);
    };

    setShow = (value) => {
        this.setState({ isShow: value });
    }

    setShowAgain = (value) => {
        this.setState({ isShowAgain: value });
    }

    setShowAddUser = (value) => {
        this.setState({ isShowAddUser: value });
        this.getUsersListHandler();
    }

    getUsersListHandler = () => {
        axios.get('http://localhost:57392/userToProject?apikey='
            + this.props.user.apikey
            + '&project_id=' + this.state.projectId)
            .then(response => {
                if (response.data.error) {
                    alert(response.data.error);
                    this.setShowAddUser(false);
                } else {
                    this.setState({ usersToAdd: response.data.data, isRequested: true });
                }
            });
    }

    getProjectHandler = (apikey, projectId) => {
        axios.get('http://localhost:57392/projectInfo?apikey='
            + this.props.user.apikey
            + '&project_id=' + projectId)
            .then(response => {
                if (response.data.error) {
                    alert(response.data.error);
                } else {
                    this.setState({ project: response.data.data, isRequested: true });
                }
            });
    }

    addUserInProjectHandler = (apikey, userId) => {

        axios.post('http://localhost:57392/admin',
            {
                apikey: this.props.user.apikey,
                project_id: this.state.projectId,
                user_id: userId
            })
            .then(response => {
                if (response.data.error) {
                    alert(response.data.error);
                } else {
                    this.setShow(false);
                    this.props.history.push(`/projects`);
                }
            });
    }

    deleteProjectHandler = (projectId) => {
        axios.post('http://localhost:57392/projectInfo',
            {
                apikey: this.props.user.apikey,
                project_id: projectId
            })
            .then(response => {
                if (response.data.error) {
                    alert(response.data.error);
                } else {
                    this.setShow(false);
                    this.props.history.push(`/projects`);
                }
            });
    }

    getProjectsListHandler = (apikey) => {
        axios.get('http://localhost:57392/project?apikey=' + this.props.user.apikey).then(response => {
            if (response.data.error) {
                alert(response.data.error);
            } else {
                this.setState({ projects: response.data.data, isRequested: true });
            }
        });
    }

    createTaskHandler = (data) => {
        axios.post('http://localhost:57392/task',
            {
                apikey: this.props.user.apikey,
                project_id: this.state.projectId,
                email: data.email,
                title: data.title,
                description: data.description,
                deadline_date: data.deadline_date
            })
            .then(response => {
                if (response.data.error) {
                    alert(response.data.error);
                } else {
                    this.setShowAgain(false);
                    this.getProjectHandler("", this.state.projectId);
                }
            });

    }

    CellFormatter = (cell, row) => {
        return (<div><a href={"/task/" + cell}>{cell}</a></div>);
    }

    CellUserFormatter = (cell, row) => {
        return (<div><a href={"/userPage/" + row.user_id}>{cell}</a></div>);
    }

    render() {
        console.log(this.props);
        if (this.state.isRequested) {
            if (!this.state.project) return <div>Страница не найдена...</div>;

            return <div className="main_fon">
                <div className="inline_block">
                    <div className="title_block">
                        <div className="text_title"> {this.state.project.project_name} </div>
                        <br />
                        <div className="text_author">
                            Автор: {this.state.project.creater_first_name + " " +
                                this.state.project.creater_last_name}
                        </div>
                    </div>
                    {
                        this.props.user.is_admin &&
                        <Button onClick={() => this.setShow(true)} className="button_padding" variant="outline-primary">Удалить проект</Button>
                    }
                </div>
                <div className="text_description">
                    {this.state.project.description}
                </div>

                <div className="users_list">
                    <div className="inline_block_users_title">
                        <div className="title_block">Список пользователей</div>
                        {
                            this.props.user.is_admin &&
                            <Button onClick={() => this.setShowAddUser(true)} className="button_padding" variant="outline-primary">Добавить пользователя</Button>
                        }
                    </div>
                    {
                        this.state.project.users_list.length !== 0 &&
                        <div>
                            {
                                this.state.project.users_list.map(user => {
                                    return <ListGroup.Item className="inline_block_elements">
                                        <a className="inline_block_users" href={"/userPage/" + user.user_id}>
                                            {user.full_name}
                                        </a>
                                        <div className="inline_block_users">{user.email}</div>
                                    </ListGroup.Item>;
                                })
                            }
                        </div>
                    }
                </div>


                <div className="inline_block_users_title">
                    <div className="title_block">
                        Задачи по проекту
                </div>
                {
                        this.props.user.is_admin &&
                    <Button onClick={() => this.setShowAgain(true)}
                        className="button_padding"
                        variant="outline-primary">
                        Добавить задачу
                    </Button>
                }

                </div>
                <div className="text_description">

                    <BootstrapTable data={this.state.project.tasks_list} striped hover>
                        <TableHeaderColumn isKey dataField='task_id' dataFormat={this.CellFormatter}>Id задачи</TableHeaderColumn>
                        <TableHeaderColumn dataField='task_name'>Название задачи</TableHeaderColumn>
                        <TableHeaderColumn dataField='date_create'>Дата постановки</TableHeaderColumn>
                        <TableHeaderColumn dataField='user_full_name' dataFormat={this.CellUserFormatter}>Исполнитель</TableHeaderColumn>
                        <TableHeaderColumn dataField='hours_count'>Затраченное время</TableHeaderColumn>
                        <TableHeaderColumn dataField='task_status_text'>Статус</TableHeaderColumn>
                    </BootstrapTable>
                </div>

                <DeleteConfirmModal show={this.state.isShow}
                    projectId={this.state.projectId}
                    onHide={() => this.setShow(false)}
                    onSave={(projectId) => this.deleteProjectHandler(projectId)}
                />

                <AddUserInProjectModal show={this.state.isShowAddUser}
                    projectId={this.state.projectId}
                    onHide={() => this.setShowAddUser(false)}
                    onSave={(apikey, userId) => this.addUserInProjectHandler(apikey, userId)}
                    usersToAdd={this.state.usersToAdd}
                />

                <CreateTaskModal show={this.state.isShowAgain}
                    onHide={() => this.setShowAgain(false)}
                    onSave={(data) => this.createTaskHandler(data)} />
            </div>;
        } else {
            return <div>Loading...</div>
        }

    }
}

const mapStateToProps = (state) => ({
    user: state.UserReduser,
    project: state.Project,
    isShow: false,
    isShowAddUser: false,
    isRequested: state.Projects ? state.Projects.IsRequested : false,
    projectId: 0,
    usersToAdd: [],
    isShowAgain: false
});
export default connect(mapStateToProps, null)(Project);