import React, { Component } from 'react';
import ListGroup from 'react-bootstrap/ListGroup';
import Button from 'react-bootstrap/Button';
import CreateProjectModal from '../CreateProjectModal';
import { connect } from 'react-redux';
import ActionTypes from '../../store/Actions/index.js';
import axios from 'axios';
import * as css from "./StylesProjects.css";
import Moment from 'react-moment';
import { Thumbnail } from 'react-bootstrap';


class Projects extends Component {
    constructor(props) {
        super(props);
        this.state = {
            projects: [],
            isShow: false,
            isRequested: false
        };
    }

    componentDidMount = () => {
        this.getProjectsListHandler();
    };

    getProjectsListHandler = () => {
        axios.get('http://localhost:57392/project?apikey=' + this.props.user.apikey).then(response => {
            this.setState({ projects: response.data.data, isRequested: true });
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


    render() {
        if (this.state.isRequested) {
            return <div className="main_fon">
                <div className="inline_block">
                    <div className="text_title_ag">Ваши проекты: </div>
                    {
                        this.props.user.is_admin &&
                        <Button className="button_padding" variant="outline-primary" onClick={() => this.setShow(true)}>Создать проект</Button>
                    }

                </div>

                <ListGroup>
                    <ListGroup.Item className="inline_block">
                        <div className="inline_element">Название проекта</div>
                        <div className="inline_element">Дата создания</div>
                        <div className="inline_element">Количество пользователей</div>
                        <div className="inline_element">Создатель</div>
                    </ListGroup.Item>
                    {
                        this.state.projects && this.state.projects.map(pr => (
                            <ListGroup.Item className="inline_block">
                                <a className="inline_element" href={"/project/" + pr.project_id}>{pr.project_name}</a>
                                <div className="inline_element">
                                    <Moment format="DD.MM.YYYY">
                                        {
                                            new Date(pr.create_date)
                                        }
                                    </Moment>
                                </div>
                                <div className="inline_element">{pr.count_users}</div>
                                <div className="inline_element">{pr.creater_first_name + ' ' + pr.creater_last_name}</div>
                            </ListGroup.Item>
                        ))
                    }
                </ListGroup>
                <CreateProjectModal show={this.state.isShow}
                    onHide={() => this.setShow(false)}
                    onSave={(name, descr) => this.createNewProjectHandler(name, descr)} />
            </div>;
        } else {
            return <div>Loading...</div>
        }

    }

}

const mapStateToProps = (state) => ({
    user: state.UserReduser,
    projects: state.Projects,
    isShow: false,
    isRequested: state.Projects ? state.Projects.IsRequested : false
});

export default connect(
    mapStateToProps,
    null
)(Projects);